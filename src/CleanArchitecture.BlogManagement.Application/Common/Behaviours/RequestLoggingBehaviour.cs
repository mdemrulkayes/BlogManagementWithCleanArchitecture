using CleanArchitecture.BlogManagement.Core.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CleanArchitecture.BlogManagement.Application.Common.Behaviours;

internal sealed class RequestLoggingBehaviour<TRequest, TResponse>(ILogger logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TRequest>
    where TResponse : Result<TRequest>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("Blog Management processing request {RequestName}", requestName);

        var result = await next();

        if (result.IsSuccess)
        {
            logger.LogInformation("Blog management request {RequestName} completed successfully", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                logger.LogError("Blog management request {RequestName} completed with error ", requestName);
            }
        }

        return result;
    }
}
