using CQRS.Application.Configuration;
using CQRS.Application.Interfaces;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CQRS.Infrastructure.Services;

public class SendGridService : ISendGridService
{
    private readonly SendGridClient _client;
    private readonly ILogger<SendGridService> _logger;

    public SendGridService(SendGridConfig sendGridConfig, ILogger<SendGridService> logger)
    {
        _client = new SendGridClient(sendGridConfig.ApiKey);
        _logger = logger;
    }

    public async Task SendAsync(IEmailMessage message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Sending SendGrid message to email: {message.To}");

        var msg = new SendGridMessage
        {
            From = new EmailAddress(message.From, null),
            Subject = message.Subject,
            PlainTextContent = !message.IsHtml
                ? message.Body
                : null,
            HtmlContent = message.IsHtml
                ? message.Body
                : null
        };

        msg.AddTos(message.To
            .Select(o => new EmailAddress(o, null))
            .ToList());

        await _client.SendEmailAsync(msg, cancellationToken);
    }

    public async Task SendAsync(string address, string subject, string body, CancellationToken cancellationToken = default)
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress("no-replay@nflweeklypicks.com", null),
            Subject = subject,
            HtmlContent = body
        };

        msg.AddTo(address);

        await _client.SendEmailAsync(msg, cancellationToken);
    }
}
