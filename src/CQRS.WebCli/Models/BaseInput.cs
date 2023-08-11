using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oakton;

namespace CQRS.WebCli.Models;

public class BaseInput : NetCoreInput
{
    private IServiceProvider? _serviceProvider;
    private IServiceScope? _serviceScope;
    private IServiceProvider Services => _serviceProvider ??= ServiceScope.ServiceProvider;
    private IServiceScope ServiceScope => _serviceScope ??= Host.Services.CreateScope();
    public IHost Host { get; set; }

    public T GetRequiredService<T>() where T : class
    {
        return Services!.GetRequiredService<T>();
    }
}
