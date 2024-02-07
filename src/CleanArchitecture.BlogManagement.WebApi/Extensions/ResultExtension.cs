using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.WebApi.Extensions;

public static class ResultExtension
{
    public static IResult ConvertToProblemDetails<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        if (result.Errors != null && result.Errors.Any())
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = "",
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Extensions = new Dictionary<string, object?>
                {
                    {"errors", result.Errors}
                }
            });
        }
        return Results.Problem(new ProblemDetails
        {
            Detail = "",
            Status = GetTitleAndStatusCode(result.Error.ErrorType).status,
            Title = GetTitleAndStatusCode(result.Error.ErrorType).title,
            Extensions = new Dictionary<string, object?>
            {
                {"errors", new[] {result.Error}}
            }
        });
    }

    private static (int status, string title) GetTitleAndStatusCode(ErrorType errorType)
    {
        var statusCode = errorType switch
        {
            ErrorType.Failure => (StatusCodes.Status400BadRequest, "Bad Request"),
            ErrorType.Validation => (StatusCodes.Status400BadRequest, "Bad Request"),
            ErrorType.NotFound => (StatusCodes.Status404NotFound, "Not Found"),
            ErrorType.Unexpected => (StatusCodes.Status500InternalServerError, "Internal Server Error"),
            ErrorType.Conflict => (StatusCodes.Status409Conflict, "Conflict"),
            ErrorType.Unauthorized => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            ErrorType.Forbidden => (StatusCodes.Status403Forbidden, "Forbidden"),
            ErrorType.None => (StatusCodes.Status400BadRequest, "Bad Request"),
            ErrorType.Custom => (StatusCodes.Status400BadRequest, "Bad Request"),
            _ => (StatusCodes.Status400BadRequest, "Bad Request")
        };

        return statusCode;
    }



}
