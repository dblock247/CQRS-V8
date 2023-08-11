using FluentValidation;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace CQRS.Application.Middleware;

public class ValidationMiddleware
{
    public void Before(ILogger logger, IServiceProvider serviceProvider, Envelope envelope)
    {
        var type = Type.GetType(envelope.MessageType!)!;
        var validatorType = typeof(IValidator<>).MakeGenericType(type);
        var validator = (IValidator?)serviceProvider.GetService(validatorType);

        if (validator is null)
            return;

        var validationContextType = typeof(ValidationContext<>).MakeGenericType(type);
        var validationContext = (IValidationContext)Activator.CreateInstance(validationContextType, new [] { envelope.Message } )!;
        var result = validator.ValidateAsync(validationContext);
    }

    public void After(ILogger logger)
    {
    }

    public void Finally(ILogger logger, Envelope envelope)
    {
    }
}
