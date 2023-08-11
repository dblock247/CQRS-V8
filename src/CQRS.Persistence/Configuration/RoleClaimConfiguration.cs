using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Persistence.Configuration;

public class RoleClaimConfiguration : EntityConfiguration<RoleClaim>
{
    public override void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        base.Configure(builder);

        builder.ToTable("RoleClaim");
    }
}
