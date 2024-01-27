using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
public sealed record GetAllPostQuery : IQuery<Result<List<PostResponse>>>;
