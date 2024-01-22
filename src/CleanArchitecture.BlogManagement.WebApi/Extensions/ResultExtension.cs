using CleanArchitecture.BlogManagement.Core.Base;
using Microsoft.AspNetCore.Mvc;

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
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Extensions = new Dictionary<string, object?>
            {
                {"errors", new[] {result.Error}}
            }
        });
    }
}
