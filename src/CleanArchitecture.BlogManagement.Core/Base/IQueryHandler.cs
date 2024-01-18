using MediatR;

namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IQueryHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
    where TCommand : IQuery<TResponse>;
