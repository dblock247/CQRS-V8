using CQRS.Application.Interfaces;
using CQRS.Domain.Entities;
using CQRS.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Persistence;

public static class Startup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, bool isDevelopment = false)
    {
        // Add DbContext
        services.AddDbContext<CQRSContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("CQRSContext"),
                o => o.UseNodaTime());

            options.EnableSensitiveDataLogging(isDevelopment);
            options.EnableDetailedErrors(isDevelopment);
        });

        services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<CQRSContext>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddUserManager<UserManager>()
            .AddRoleManager<RoleManager>()
            .AddSignInManager<SignInManager>()
            .AddDefaultTokenProviders();

        // services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true);

        // add transients
        services.AddScoped<ICQRSContext, CQRSContext>();

        return services;
    }
}
