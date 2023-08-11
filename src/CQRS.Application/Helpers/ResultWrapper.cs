using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace CQRS.Application.Helpers;

public class ResultWrapper<T> : BaseResult
{
    public ResultWrapper()
    {
    }

    /// <summary>
    /// Wrap a success result.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ResultWrapper<T> Ok(T result)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.Success,
            Result = result
        };

        return rc;
    }

    /// <summary>
    /// Wrap a NotFound result.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ResultWrapper<T> NotFound(string message)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.NotFound,
            Message = new string[] { message },
            Title = "Not Found"
        };

        return rc;
    }

    /// <summary>
    /// Wrap a BadRequest result
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ResultWrapper<T> BadRequest(string message)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.BadRequest,
            Message = new string[] { message },
            Title = "Bad Request"
        };

        return rc;
    }

    /// <summary>
    /// Wrap a ValidationFailures result.
    /// </summary>
    /// <param name="failures"></param>
    /// <returns></returns>
    public static ResultWrapper<T> ValidationFailure(IList<ValidationFailure> failures)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.ValidationErrors,
            Message = failures.Select(f => f.ErrorMessage)
                .ToList(),
            ValidationFailures = failures,
            Title = "Validation Error"
        };

        return rc;
    }

    public static ResultWrapper<T> Forbidden(string message = default)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.Forbidden,
            Message = new string[] { message },
            Title = "Forbidden"
        };

        return rc;
    }

    public static ResultWrapper<T> Forbidden(string message, string detail)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.Forbidden,
            Message = new string[] { message },
            Title = "Forbidden",
            Detail = detail
        };

        return rc;
    }


    public static ResultWrapper<T> DuplicateKeyError(string message = default)
    {
        var rc = new ResultWrapper<T>()
        {
            Status = ResultWrapperStatus.DuplicateKeyError,
            Message = new string[] { message },
            Title = "Duplicate"
        };

        return rc;
    }

    public T Result { get; private set; }
    public IList<string> Message { get; private set; }
    public string Title { get; private set; }
    public string Detail { get; private set; }
    public IList<ValidationFailure> ValidationFailures { get; private set; }
}
