using Microsoft.Extensions.Logging;
using Wolverine;

namespace CQRS.Application.Middleware;

public class UnhandledExceptionMiddleware
{
    public void Before(ILogger logger)
    {
    }

    public void After(ILogger logger)
    {
    }

    public void Finally(ILogger logger, Envelope envelope)
    {
    }
}
