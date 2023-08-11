using System.Reflection;
using CQRS.Application;
using CQRS.Infrastructure;
using CQRS.Persistence;
using Jetpack.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CQRS.Sandbox;

public class Startup : IHostBuilderConfiguration
{
    public void BuildConfiguration(HostBuilderContext hostBuilderContext, IConfigurationBuilder builder)
    {
        builder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)!)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostBuilderContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetCallingAssembly(), optional: true);
    }

    public void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging();

        services.AddApplication();
        services.AddInfrastructure(context.Configuration);
        services.AddPersistence(context.Configuration, context.HostingEnvironment.IsDevelopment());

        // services.AddMarten(options =>
        //     {
        //         options.Connection(configuration.GetConnectionString("WolverineContext")!);
        //         options.DatabaseSchemaName = "documents";
        //         options.UseDefaultSerialization(casing: Casing.CamelCase, enumStorage: EnumStorage.AsString);
        //         options.Schema.Include<Registry>(); })
        //     .InitializeWith(new Seed()
        //     );
    }
}
