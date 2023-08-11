using NodaTime;

namespace CQRS.Domain.Interfaces;

public interface ISoftDeletable : IAuditable
{
    Instant? DeletedOnUtc { get; set; }

    void Delete();
}
