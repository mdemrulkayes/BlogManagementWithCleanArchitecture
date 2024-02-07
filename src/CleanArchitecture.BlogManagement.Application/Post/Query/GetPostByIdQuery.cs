using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
public sealed record GetPostByIdQuery(long PostId) : IQuery<Result<PostResponse>>;
