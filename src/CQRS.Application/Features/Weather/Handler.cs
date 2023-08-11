using Wolverine;

namespace CQRS.Application.Features.Weather;

public class Handler : IWolverineHandler
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<List<Response>> HandleAsync(Request request)
    {
        var response = Enumerable.Range(1, 5).Select(index => new Response
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();

        return Task.FromResult(response);
    }
}
