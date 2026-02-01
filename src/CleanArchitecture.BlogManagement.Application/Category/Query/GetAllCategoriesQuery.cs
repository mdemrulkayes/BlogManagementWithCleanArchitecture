using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;

public sealed record GetAllCategoriesQuery(int PageNumber = 1, int PageSize = 10) 
    : IQuery<Result<PagedListDto<CategoryResponse>>>;
