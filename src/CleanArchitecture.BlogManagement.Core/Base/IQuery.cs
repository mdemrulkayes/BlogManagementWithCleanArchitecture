using MediatR;

namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IQuery<out TResponse> : IRequest<TResponse>;
