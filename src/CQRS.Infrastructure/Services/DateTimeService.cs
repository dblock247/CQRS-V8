using CQRS.Application.Interfaces;

namespace CQRS.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
