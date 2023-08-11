using NodaTime;

namespace CQRS.Domain.Interfaces;

public interface IAuditable : IEntity
{
    /// <summary>
    /// User that created the record.
    /// </summary>
    string CreatedBy { get; set; }

    /// <summary>
    /// Creation date and time.
    /// </summary>
    Instant CreatedOnUtc { get; set; }

    /// <summary>
    /// User that updated the record.
    /// </summary> d
    string? ModifiedBy { get; set; }

    /// <summary>
    /// Updated date and time.
    /// </summary>
    Instant? ModifiedOnUtc { get; set; }
}
