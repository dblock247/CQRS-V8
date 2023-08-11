using CQRS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace CQRS.Domain.Entities;

public class Role : IdentityRole<Guid>, ISoftDeletable
{
    public string CreatedBy { get; set; } = "N/A";
    public Instant? DeletedOnUtc { get; set; }
    public Instant CreatedOnUtc { get; set; }
    public string? ModifiedBy { get; set; }
    public Instant? ModifiedOnUtc { get; set; }

    public void Delete()
    {
        DeletedOnUtc = SystemClock.Instance.GetCurrentInstant();
    }
}
