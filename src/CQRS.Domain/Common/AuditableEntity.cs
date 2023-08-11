using CQRS.Domain.Interfaces;
using NodaTime;

namespace CQRS.Domain.Common;

public class AuditableEntity : Entity, IAuditable
{
    public string CreatedBy { get; set; } = "N/A";
    public Instant CreatedOnUtc { get; set; }
    public string? ModifiedBy { get; set; }
    public Instant? ModifiedOnUtc { get; set; }
}
