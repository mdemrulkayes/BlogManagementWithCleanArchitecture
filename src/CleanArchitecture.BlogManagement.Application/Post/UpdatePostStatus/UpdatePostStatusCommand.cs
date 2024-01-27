using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdatePostStatus;
public sealed record UpdatePostStatusCommand(long PostId, PostStatus Status) : ICommand<Result<long>>;
