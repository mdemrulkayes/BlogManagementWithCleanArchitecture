using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Tag;
using Microsoft.Extensions.Caching.Memory;
using SharedKernel;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Create;
internal sealed class CreateTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache) : ICommandHandler<CreateTagCommand, Result<TagResponse>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<TagResponse>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = TagCore.Create(request.Name, request.Description);

        if (!tag.IsSuccess || tag.Value is null)
        {
            return tag.Error;
        }

        var dataTag = tag.Value;

        await repository.Add(dataTag);
        await unitOfWork.CommitAsync(cancellationToken);

        memoryCache.Remove(TagConstants.TagCacheKey);

        return mapper.Map<TagCore, TagResponse>(dataTag);
    }
}
