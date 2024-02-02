using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
public sealed record GetAllPostQuery : QueryStringParameter, IQuery<Result<PagedListDto<PostResponse>>>;
