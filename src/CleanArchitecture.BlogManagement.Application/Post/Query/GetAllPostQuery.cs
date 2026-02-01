using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;

public sealed record GetAllPostQuery(int PageNumber = 1, int PageSize = 10) 
    : IQuery<Result<PagedListDto<PostResponse>>>;
