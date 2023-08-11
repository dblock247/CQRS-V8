using CQRS.Application.Configuration;
using CQRS.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Stripe;

namespace CQRS.Infrastructure.Services;

public class StripeService : IPaymentService
{
    private readonly StripeConfig _stripeConfig;
    private readonly ILogger<StripeService> _logger;
    private const int StripeMultiplier = 100;

    public StripeService(StripeConfig stripeConfig, ILogger<StripeService> logger)
    {
        _stripeConfig = stripeConfig;
        _logger = logger;
    }

    public async Task<string> CreatePaymentIntent(ITransaction transaction, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Creating payment intent for amount: {transaction.Amount:C2}");
        var paymentIntentCreateOptions = new PaymentIntentCreateOptions
        {
            // stripe amount is in pennies. You need to multiply by 100 to get to dollars.
            Amount = (long)(transaction.Amount * StripeMultiplier),
            Currency = "usd",
            PaymentMethodTypes = new List<string> { "card" }
        };

        var options = new RequestOptions
        {
            ApiKey = _stripeConfig.SecretKey,
            IdempotencyKey = $"{transaction.Id}"
        };

        var service = new PaymentIntentService();
        var response = await service.CreateAsync(paymentIntentCreateOptions, options, cancellationToken);

        return response.Id;
    }
}
