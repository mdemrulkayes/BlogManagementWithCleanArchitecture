using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Category.Create;

public sealed record CreateCategoryCommand(string Name, string Description) : ICommand<Result<CategoryResponse>>;

