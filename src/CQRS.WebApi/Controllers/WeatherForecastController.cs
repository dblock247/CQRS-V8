using CQRS.Application.Features.Weather;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CQRS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMessageBus _bus;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IMessageBus bus, ILogger<WeatherForecastController> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<List<Response>> Get()
    {
        return await _bus.InvokeAsync<List<Response>>(new Request());
    }
}
