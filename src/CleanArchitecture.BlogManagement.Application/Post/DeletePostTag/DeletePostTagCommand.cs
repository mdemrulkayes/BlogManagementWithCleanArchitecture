using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.DeletePostTag;
public sealed record DeletePostTagCommand(long PostId, long TagId) : ICommand<Result<long>>;
