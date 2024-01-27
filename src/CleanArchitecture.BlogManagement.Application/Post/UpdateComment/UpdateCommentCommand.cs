using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdateComment;
public sealed record UpdateCommentCommand(long PostId, long CommentId, string CommentText) : ICommand<Result<long>>;
