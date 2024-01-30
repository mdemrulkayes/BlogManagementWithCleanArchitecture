using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal sealed class GetAllTagsQueryHandler(ITagRepository repository, IMapper mapper) : IQueryHandler<GetAllTagsQuery, Result<List<TagResponse>>>
{
    public async Task<Result<List<TagResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var results = await repository.GetAllTags(cancellationToken);
        return mapper.Map<List<TagResponse>>(results);
    }
}
