using CQRS.Application.Features.Weather;
using Wolverine;

namespace CQRS.Worker;

public class Worker : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly ILogger<Worker> _logger;

    public Worker(IMessageBus bus, ILogger<Worker> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var request = new Request();
        await _bus.InvokeAsync<List<Response>>(request, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
