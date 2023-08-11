using CQRS.Domain.Interfaces;

namespace CQRS.Domain.Common;

public class Entity : IEntity
{
    public Guid Id { get; set; }
}
