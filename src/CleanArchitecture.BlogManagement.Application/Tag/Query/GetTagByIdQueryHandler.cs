using AutoMapper;
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
        if (!memoryCache.TryGetValue($"{TagConstants.TagDetailsKey}{request.TagId}", out var tagDetails))
        {
            tagDetails = await repository.FirstOrDefaultAsync(x => x.TagId == request.TagId);

            if (tagDetails == null)
            {
                return TagErrors.TagNotFound;
            }

            memoryCache.Set($"{TagConstants.TagDetailsKey}{request.TagId}", tagDetails, new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromHours(2)
            });
        }
       
        return mapper.Map<TagResponse>(tagDetails);
    }
}
