using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdatePostStatus;
public sealed record UpdatePostStatusCommand(long PostId, PostStatus Status) : ICommand<Result<long>>;
