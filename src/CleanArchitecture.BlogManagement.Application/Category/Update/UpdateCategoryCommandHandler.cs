using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.BlogManagement.Application.Category.Update;
internal sealed class UpdateCategoryCommandHandler(ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IMemoryCache memoryCache) : ICommandHandler<UpdateCategoryCommand, Result<CategoryResponse>>
{
    public async Task<Result<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =
            await repository.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId);
        if (category == null)
        {
            return CategoryErrors.NotFound;
        }

        var updatedCategory = category.Update(request.Name, request.Description);
        if (!updatedCategory.IsSuccess || updatedCategory.Value is null)
        {
            return updatedCategory.Error;
        }
        await repository.Update(updatedCategory.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        memoryCache.Remove(CategoryConstants.CategoryCacheKey);

        return mapper.Map<CategoryResponse>(updatedCategory.Value);
    }
}
