using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BlogManagement.Application.Common.Behaviours;
public sealed class LoggingBehaviour<TRequest>(ILogger logger) : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("Blog Management Incoming request from {Name}", requestName);
        return Task.CompletedTask;
    }
}
