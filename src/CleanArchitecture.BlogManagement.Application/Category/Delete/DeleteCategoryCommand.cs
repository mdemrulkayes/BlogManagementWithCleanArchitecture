using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Category.Delete;
public sealed record DeleteCategoryCommand(long CategoryId) : ICommand<Result<bool>>;
