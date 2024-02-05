using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.BlogManagement.Application.Category.Delete;
internal sealed class DeleteCategoryCommandHandler(ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    IMemoryCache memoryCache) : ICommandHandler<DeleteCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =
            await repository.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId);
        if (category == null)
        {
            return CategoryErrors.NotFound;
        }

        await repository.Delete(category);
        await unitOfWork.CommitAsync(cancellationToken);

        memoryCache.Remove(CategoryConstants.CategoryCacheKey);

        return true;
    }
}
