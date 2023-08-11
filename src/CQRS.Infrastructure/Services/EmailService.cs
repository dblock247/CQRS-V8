using CQRS.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace CQRS.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly ISmtpClient _client;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
        _client = new SmtpClient();
    }

    public async Task SendAsync(IEmailMessage message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SendAsync(string address, string subject, string body, CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(""));
        message.To.Add(MailboxAddress.Parse(address));
        message.Subject = subject;
        message.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

        await Task.CompletedTask;
    }

    ~EmailService()
    {
        _client?.Dispose();
    }
}
