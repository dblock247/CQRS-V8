using CQRS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace CQRS.Domain.Entities;

public class UserClaim : IdentityUserClaim<Guid>, ISoftDeletable
{
    public new Guid Id { get; set; }
    public Instant? DeletedOnUtc { get; set; }
    public string CreatedBy { get; set; } = "N/A";
    public Instant CreatedOnUtc { get; set; }
    public string? ModifiedBy { get; set; }
    public Instant? ModifiedOnUtc { get; set; }

    public void Delete()
    {
        DeletedOnUtc = SystemClock.Instance.GetCurrentInstant();
    }
}
