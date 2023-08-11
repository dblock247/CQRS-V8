using System.Diagnostics;
using CQRS.Sandbox;
using Jetpack.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var app = Host.CreateDefaultBuilder()
    .UseStartup<Startup>()
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithCorrelationId()
        .Enrich.WithCorrelationIdHeader()
        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name!)
        .Enrich.WithProperty("Environment", context.HostingEnvironment)
        .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached))
    .Build();

var logger = app.Services.GetRequiredService<ILogger>();
logger.Warning("Hello World!!!");
