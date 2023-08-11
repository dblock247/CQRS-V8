using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CQRS.Persistence;

public class CQRSContextFactory : IDesignTimeDbContextFactory<CQRSContext>
{
    public CQRSContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        if (environment == "Development")
            builder.AddUserSecrets(Assembly.Load("CQRS.WebApi"), optional: true);

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("CQRSContext");
        var optionsBuilder = new DbContextOptionsBuilder<CQRSContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CQRSContext(optionsBuilder.Options);
    }
}
