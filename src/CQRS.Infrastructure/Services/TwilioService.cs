using CQRS.Application.Configuration;
using CQRS.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;

namespace CQRS.Infrastructure.Services;

public class TwilioService : ISmsService
{
    private readonly TwilioConfig _twilioConfig;
    private readonly ILogger<TwilioService> _logger;

    public TwilioService(TwilioConfig twilioConfig, ILogger<TwilioService> logger)
    {
        _twilioConfig = twilioConfig;
        _logger = logger;
        TwilioClient.Init(twilioConfig.AccountSid, twilioConfig.AuthToken);
    }

    public async Task SendAsync(ISmsMessage message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Sending twilio message to: {message.To}");
        await MessageResource.CreateAsync(
            to: new PhoneNumber(message.To),
            @from: new PhoneNumber(_twilioConfig.From),
            body: message.Body);
    }

    public async Task<string> SendVerifyCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Sending phone verification to: {phoneNumber}");
        var verification = await VerificationResource.CreateAsync(
            to: $"+1{phoneNumber}",
            channel: "sms",
            pathServiceSid: _twilioConfig.VerifySid);

        return verification.Status;
    }

    public async Task<string> CheckVerifyCodeAsync(string phoneNumber, string code, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Checking verification for phone number: {phoneNumber}");
        var verificationCheck = await VerificationCheckResource.CreateAsync(
            to: $"+1{phoneNumber}",
            code: code,
            pathServiceSid: _twilioConfig.VerifySid);

        return verificationCheck.Status;
    }
}
