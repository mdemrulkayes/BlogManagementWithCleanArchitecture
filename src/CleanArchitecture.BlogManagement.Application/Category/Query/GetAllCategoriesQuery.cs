using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
public sealed record GetAllCategoriesQuery : IQuery<Result<List<CategoryResponse>>>;
