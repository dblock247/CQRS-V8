using System.Reflection;
using CQRS.Application.Interfaces;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;

namespace CQRS.Persistence;

public class CQRSContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, ICQRSContext
{
    public CQRSContext(DbContextOptions<CQRSContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            var zone = DateTimeZoneProviders.Tzdb["UTC"];
            var clock = SystemClock.Instance.InZone(zone);

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "N/A";
                    entry.Entity.CreatedOnUtc = clock.GetCurrentInstant();
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = "N/A";
                    entry.Entity.ModifiedOnUtc = clock.GetCurrentInstant();
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
