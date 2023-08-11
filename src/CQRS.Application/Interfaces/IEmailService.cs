using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Interfaces;

public interface IEmailService
{
    Task SendAsync(IEmailMessage message, CancellationToken cancellationToken = default);
    Task SendAsync(string address, string subject, string body, CancellationToken cancellationToken = default);
}
