using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BlogManagement.Application.Common.Exceptions;
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Error occurred. Message: {message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Internal server error.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unhandled exception occurred. Please contact with admin"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
