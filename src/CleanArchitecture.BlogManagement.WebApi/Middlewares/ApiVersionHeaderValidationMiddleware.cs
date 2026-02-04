using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.BlogManagement.WebApi.Middlewares;

public sealed class ApiVersionHeaderValidationMiddleware(RequestDelegate next, ILogger<ApiVersionHeaderValidationMiddleware> logger)
{
    public RequestDelegate Next { get; } = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip version validation for OpenAPI and Scalar documentation endpoints
        var path = context.Request.Path.Value?.ToLower();
        if (path?.Contains("/openapi") == true || 
            path?.Contains("/scalar") == true ||
            path?.Contains("/swagger") == true)
        {
            await Next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("x-api-version", out var apiVersion))
        {
            logger.LogWarning("API version header is missing. Using default version 1.");
            // Set default version to 1 if not provided
            context.Request.Headers["x-api-version"] = "1";
        }
        
        await Next(context);
    }
}
