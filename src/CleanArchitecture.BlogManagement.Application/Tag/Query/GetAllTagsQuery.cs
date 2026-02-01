using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;

public sealed record GetAllTagsQuery(int PageNumber = 1, int PageSize = 10) 
    : QueryStringParameter(PageNumber, PageSize), IQuery<Result<PagedListDto<TagResponse>>>;