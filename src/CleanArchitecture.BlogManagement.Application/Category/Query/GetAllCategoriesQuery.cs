using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
public sealed record GetAllCategoriesQuery : QueryStringParameter, IQuery<Result<PagedListDto<CategoryResponse>>>;
