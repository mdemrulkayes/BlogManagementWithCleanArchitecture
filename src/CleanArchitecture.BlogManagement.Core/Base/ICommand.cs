using MediatR;

namespace CleanArchitecture.BlogManagement.Core.Base;

public interface ICommand<out TResponse> : IRequest<TResponse>;
