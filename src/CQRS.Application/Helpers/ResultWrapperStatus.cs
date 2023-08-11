namespace CQRS.Application.Helpers;

public enum ResultWrapperStatus
{
    Success,
    NotFound,
    ValidationErrors,
    BadRequest,
    Forbidden,
    DuplicateKeyError
}
