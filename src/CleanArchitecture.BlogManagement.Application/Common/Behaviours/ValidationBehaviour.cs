using MediatR;

namespace CleanArchitecture.BlogManagement.Application.Common.Behaviours;
public sealed class ValidationBehaviour<TRequest, TResponse> :  IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
