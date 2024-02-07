using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Common.Behaviours;

public class RequestLoggingBehaviour<TRequest, TResponse>(ILogger<RequestLoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IBaseResult
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