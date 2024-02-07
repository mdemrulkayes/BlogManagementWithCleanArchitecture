using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
public sealed record GetAllPostQuery : QueryStringParameter, IQuery<Result<PagedListDto<PostResponse>>>;
