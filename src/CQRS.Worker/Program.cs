using System.Diagnostics;
// using CQRS.Application.Actions;
using CQRS.Application.Middleware;
using CQRS.Worker;
using Jetpack.Extensions.Hosting;
using Oakton;
using Serilog;
using Wolverine;
using Wolverine.FluentValidation;

var app =  Host.CreateDefaultBuilder()
    .UseStartup<Startup>()
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithCorrelationId()
        .Enrich.WithCorrelationIdHeader()
        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name!)
        .Enrich.WithProperty("Environment", context.HostingEnvironment)
        .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached))
    .UseWolverine(options =>
    {
        options.Discovery.IncludeAssembly(typeof(Program).Assembly);
        options.UseFluentValidation();

        // options.Services.AddSingleton(typeof(IFailureAction<>), typeof(ValidationFailureAction<>));

        options.Policies.AddMiddleware<PerformanceMiddleware>();
        options.Policies.AddMiddleware<ValidationMiddleware>();
        options.Policies.AddMiddleware<UnhandledExceptionMiddleware>();
        options.Policies.AddMiddleware<LoggingMiddleware>();
        options.LocalQueue("important")
            .UseDurableInbox();
    })
    .ApplyOaktonExtensions()
    .Build();

return await app.RunOaktonCommands(args);
