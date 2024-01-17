using Serilog.Context;

namespace CleanArchitecture.BlogManagement.WebApi.Middlewares;

public sealed class RequestLogContextMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier, true))
        {
            return next(context);
        }
    }
}
