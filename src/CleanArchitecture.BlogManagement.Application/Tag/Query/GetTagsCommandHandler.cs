using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal class GetTagsCommandHandler(IRepository repository, IMapperBase mapper) : IQueryHandler<GetTagsCommand, Result<List<TagResponse>>>
{
    public async Task<Result<List<TagResponse>>> Handle(GetTagsCommand request, CancellationToken cancellationToken)
    {
        var results = await repository.FindAllAsync<TagCore>(cancellationToken);
        return mapper.Map<List<TagCore>, List<TagResponse>>(results);
    }
}
