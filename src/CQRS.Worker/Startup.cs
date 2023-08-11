using System.Reflection;
using CQRS.Application;
using CQRS.Infrastructure;
using CQRS.Persistence;
using Jetpack.Extensions.Hosting;

namespace CQRS.Worker;

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

        services.AddHostedService<Worker>();
    }
}
