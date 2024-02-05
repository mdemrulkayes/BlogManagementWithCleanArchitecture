using AutoMapper;
using CleanArchitecture.BlogManagement.Application.Common;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Tag;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal class GetTagByIdQueryHandler(ITagRepository repository,
    IMapper mapper,
    IMemoryCache memoryCache) : ICommandHandler<GetTagByIdQuery, Result<TagResponse>>
{
    public async Task<Result<TagResponse>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tagDetails = await memoryCache.GetOrCreateAsync($"{TagConstants.TagDetailsKey}{request.TagId}",
            async cacheEntry =>
            {
                cacheEntry.SetAllMemoryCacheOptions();
                return await repository.FirstOrDefaultAsync(x => x.TagId == request.TagId);
            });

        if (tagDetails == null)
        {
            return TagErrors.TagNotFound;
        }

        return mapper.Map<TagResponse>(tagDetails);
    }
}
