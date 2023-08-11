using CQRS.Domain.Interfaces;
using NodaTime;

namespace CQRS.Domain.Common;

public class SoftDeletable : AuditableEntity, ISoftDeletable
{

    public Instant? DeletedOnUtc { get; set; }

    public void Delete()
    {
        DeletedOnUtc = SystemClock.Instance.GetCurrentInstant();
    }
}
