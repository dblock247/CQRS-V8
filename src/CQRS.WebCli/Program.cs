// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
// using CQRS.Application.Actions;
using CQRS.Application.Features.Weather;
using CQRS.Application.Interfaces;
using CQRS.Application.Middleware;
using CQRS.WebCli;
using CQRS.WebCli.Features.Weather;
using CQRS.WebCli.Models;
// using Jetpack.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oakton;
using Serilog;
using Wolverine;
using Wolverine.FluentValidation;

var executor = CommandExecutor.For(_ =>
{
    _.RegisterCommands(typeof(Program).Assembly);
    _.DefaultCommand = typeof(Command);

    _.ConfigureRun = run =>
    {
        if (run.Input is not BaseInput input)
            return;

        input.Host = Host.CreateDefaultBuilder()
            // .UseStartup<Startup>()
            .UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name!)
                .Enrich.WithProperty("Environment", context.HostingEnvironment)
                .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached))
            .UseWolverine(options =>
            {
                options.Discovery.IncludeAssembly(typeof(ICQRSContext).Assembly);
                options.UseFluentValidation();

                options.Policies.AddMiddleware<PerformanceMiddleware>();
                options.Policies.AddMiddleware<ValidationMiddleware>();
                options.Policies.AddMiddleware<UnhandledExceptionMiddleware>();
                options.Policies.AddMiddleware<LoggingMiddleware>();
                options.LocalQueue("important")
                    .UseDurableInbox();

                // Console.WriteLine(options.DescribeHandlerMatch(typeof(Handler)));
            })
            .ApplyOaktonExtensions()
            .StartAsync()
            .GetAwaiter()
            .GetResult();
    };
});

return executor.Execute(args);
