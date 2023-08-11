using CQRS.Application.Interfaces;
using CQRS.Application.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddScoped<UserSession>();
        services.AddScoped<IUserSession>(x => x.GetRequiredService<UserSession>());

        return services;
    }
}
