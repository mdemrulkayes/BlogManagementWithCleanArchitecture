using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal sealed class GetAllTagsQueryHandler(ITagRepository repository, IMapper mapper) : IQueryHandler<GetAllTagsQuery, Result<PaginatedList<TagResponse>>>
{
    public async Task<Result<PaginatedList<TagResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var results = await repository.GetAllTags(request.PageNumber, request.PageSize, cancellationToken);
        return mapper.Map<PaginatedList<TagResponse>>(results);
    }
}
