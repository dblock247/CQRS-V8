using CQRS.Application.Features.Weather;
using Oakton;
using Serilog;
using Spectre.Console;
using Wolverine;

namespace CQRS.WebCli.Features.Weather;

[Description("", Name = "weather")]
public class Command : OaktonAsyncCommand<Input>
{
    public Command()
    {
        Usage("Weather")
            .ValidFlags(x => x.CityFlag);
    }

    public override async Task<bool> Execute(Input input)
    {
        var logger = input.GetRequiredService<ILogger>();
        var bus = input.GetRequiredService<IMessageBus>();

        var request = new Request { City = input.CityFlag };
        var response = await bus.InvokeAsync<List<Response>>(request);

        var table = new Table();
        table.AddColumn("Date");
        table.AddColumn("Temp (F)");
        table.AddColumn("Temp (C)");
        table.AddColumn("Summary");

        foreach (var record in response)
        {
            table.AddRow($"{record.Date}"
                , $"{record.TemperatureF}"
                , $"{record.TemperatureC}"
                , $"{record.Summary}");
        }

        logger.Information("Temperatures for city: {City}", input.CityFlag);
        AnsiConsole.Write(table);

        return true;
    }
}
