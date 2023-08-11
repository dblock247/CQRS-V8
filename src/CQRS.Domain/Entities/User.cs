using CQRS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace CQRS.Domain.Entities;

public class User : IdentityUser<Guid>, ISoftDeletable
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
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
