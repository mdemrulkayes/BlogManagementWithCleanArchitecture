using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Category.Update;

public sealed record UpdateCategoryCommand(long CategoryId, string Name, string Description) : ICommand<Result<CategoryResponse>>;
