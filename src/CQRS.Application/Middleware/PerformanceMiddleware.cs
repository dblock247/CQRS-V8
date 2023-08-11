using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace CQRS.Application.Middleware;

public class PerformanceMiddleware
{
    private readonly Stopwatch _stopwatch = new();

    public void Before(ILogger logger, Envelope envelope)
    {
        _stopwatch.Start();
        logger.LogDebug("Starting Middleware");
    }

    public void After(ILogger logger)
    {
        logger.LogDebug("After Middleware");
    }

    public void Finally(ILogger logger, Envelope envelope)
    {
        _stopwatch.Stop();
        logger.LogDebug("Envelope {Id} / {MessageType} ran in {Duration} milliseconds",
            envelope.Id, envelope.MessageType, _stopwatch.ElapsedMilliseconds);
    }
}
