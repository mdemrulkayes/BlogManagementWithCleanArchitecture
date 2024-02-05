using AutoMapper;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
internal sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository,
    IMemoryCache memoryCache,
    IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, Result<PagedListDto<CategoryResponse>>>
{
    public async Task<Result<PagedListDto<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (!memoryCache.TryGetValue(CategoryConstants.CategoryCacheKey, out var result))
        {
            result = await repository.GetAllCategories(request.PageSize, request.PageNumber, cancellationToken);

            memoryCache.Set(CategoryConstants.CategoryCacheKey, result , new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(2)
            });
        }
        return mapper.Map<PagedListDto<CategoryResponse>>(result);
    }
}
