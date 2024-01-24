using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CleanArchitecture.BlogManagement.Application.Common.Exceptions;
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Error occurred. Message: {message}", exception.Message);

        var errorDetails = environment.IsDevelopment() ? JsonConvert.SerializeObject(exception) : "Please contact with admin";

        var problemDetails = new ProblemDetails
        {
            Title = "Internal server error.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = $"An unhandled exception occurred. {errorDetails} "
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
