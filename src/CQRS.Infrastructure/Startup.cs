using CQRS.Application.Configuration;
using CQRS.Infrastructure.Helpers;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var ssoConfig = configuration.GetSection("FlurlConfig").Get<FlurlConfiguration>();
        if (ssoConfig.IgnoreSslErrors)
            FlurlHttp.Configure(settings => settings.HttpClientFactory = new UntrustedCertClientFactory());

        // add singletons
        services.AddSingleton(ssoConfig);

        return services;
    }
}
