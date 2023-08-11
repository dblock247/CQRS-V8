using FluentValidation;
using FluentValidation.Results;
using Wolverine.FluentValidation;

namespace CQRS.Application.Actions;

public class ValidationFailureAction<T> : IFailureAction<T>
{
    public void Throw(T message, IReadOnlyList<ValidationFailure> failures)
    {
        throw new ValidationException(failures);
    }
}
