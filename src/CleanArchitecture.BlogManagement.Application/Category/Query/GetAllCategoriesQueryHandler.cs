using AutoMapper;
using CleanArchitecture.BlogManagement.Application.Common;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Category;
using Microsoft.Extensions.Caching.Memory;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
internal sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository,
    IMemoryCache memoryCache,
    IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, Result<PagedListDto<CategoryResponse>>>
{
    public async Task<Result<PagedListDto<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await memoryCache.GetOrCreateAsync(CategoryConstants.CategoryCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SetAllMemoryCacheOptions();
                return await repository.GetAllCategories(request.PageSize, request.PageNumber, cancellationToken);
            });

        return mapper.Map<PagedListDto<CategoryResponse>>(result);
    }
}
