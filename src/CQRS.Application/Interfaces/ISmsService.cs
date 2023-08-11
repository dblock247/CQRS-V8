namespace CQRS.Application.Interfaces;

public interface ISmsService
{
    Task SendAsync(ISmsMessage message, CancellationToken cancellationToken = default);
    Task<string> SendVerifyCodeAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<string> CheckVerifyCodeAsync(string phoneNumber, string code, CancellationToken cancellationToken = default);
}
