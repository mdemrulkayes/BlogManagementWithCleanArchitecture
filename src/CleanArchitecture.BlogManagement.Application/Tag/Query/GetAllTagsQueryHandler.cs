using AutoMapper;
using CleanArchitecture.BlogManagement.Application.Common;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Tag;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal sealed class GetAllTagsQueryHandler(ITagRepository repository,
    IMapper mapper,
    IMemoryCache memoryCache) : IQueryHandler<GetAllTagsQuery, Result<PagedListDto<TagResponse>>>
{
    public async Task<Result<PagedListDto<TagResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var results = await memoryCache.GetOrCreateAsync(TagConstants.TagCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SetAllMemoryCacheOptions();
                return await repository.GetAllTags(request.PageNumber, request.PageSize, cancellationToken);
            });

        return mapper.Map<PagedListDto<TagResponse>>(results);
    }
}
