using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.CreateComment;
public sealed record CreateCommentCommand(long PostId, string Text) : ICommand<Result<long>>;
