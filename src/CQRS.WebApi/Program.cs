using System.Diagnostics;
using CQRS.Application.Features.Weather;
using CQRS.Application.Interfaces;
using CQRS.Application.Middleware;
using CQRS.WebApi;
using Jetpack.Extensions.Hosting;
using Oakton;
using Serilog;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .UseStartup<Startup>()
    .UseSerilog((context, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithCorrelationId()
        .Enrich.WithCorrelationIdHeader()
        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name!)
        .Enrich.WithProperty("Environment", context.HostingEnvironment)
        .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached)
    )
    .UseWolverine(options=>
    {
        options.Discovery.IncludeAssembly(typeof(ICQRSContext).Assembly);
        options.UseFluentValidation();

        options.Policies.AddMiddleware<PerformanceMiddleware>();
        options.Policies.AddMiddleware<ValidationMiddleware>();
        options.Policies.AddMiddleware<UnhandledExceptionMiddleware>();
        options.Policies.AddMiddleware<LoggingMiddleware>();
        // options.LocalQueue("important")
        // .UseDurableInbox();

        Console.WriteLine(options.DescribeHandlerMatch(typeof(Handler)));

        options.UseFluentValidation();
    })
    .ApplyOaktonExtensions();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

return await app.RunOaktonCommands(args);
