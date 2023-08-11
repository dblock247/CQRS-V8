using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Interfaces;

public interface IPaymentService
{
    Task<string> CreatePaymentIntent(ITransaction transaction, CancellationToken cancellationToken = default);
}
