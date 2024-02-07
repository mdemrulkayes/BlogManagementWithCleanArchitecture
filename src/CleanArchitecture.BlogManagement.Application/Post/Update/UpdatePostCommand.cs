using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Update;
public sealed record UpdatePostCommand(long PostId, string Title, string Slug, string Text) : ICommand<Result<long>>;
