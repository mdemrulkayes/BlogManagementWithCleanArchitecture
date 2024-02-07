using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
public sealed record GetAllCategoriesQuery : QueryStringParameter, IQuery<Result<PagedListDto<CategoryResponse>>>;
