using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.BlogManagement.WebApi.Middlewares;

public sealed class ApiVersionHeaderValidationMiddleware(RequestDelegate next, ILogger<ApiVersionHeaderValidationMiddleware> logger)
{
    public RequestDelegate Next { get; } = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("x-api-version", out var apiVersion))
        {
            logger.LogWarning("API version header is missing.");
            var problemDetails = new ProblemDetails()
            {
                Title = "ApiVersionUnspecified",
                Detail = "An API version is required, but was not specified",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://docs.api-versioning.org/problems#unspecified"
            };
            context.Response.StatusCode = problemDetails.Status.Value;

            await context.Response.WriteAsJsonAsync(problemDetails);
            return;
        }
        await Next(context);
    }
}
