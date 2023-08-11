using Microsoft.Extensions.Logging;
using Wolverine;

namespace CQRS.Application.Middleware;

public class LoggingMiddleware
{
    public void Before(ILogger logger)
    {
        logger.LogDebug("Starting Middleware");
    }

    public void After(ILogger logger)
    {
        logger.LogDebug("After Middleware");
    }

    public void Finally(ILogger logger, Envelope envelope)
    {

    }
}
