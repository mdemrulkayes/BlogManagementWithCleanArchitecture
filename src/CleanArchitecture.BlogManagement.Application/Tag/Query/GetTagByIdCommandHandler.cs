using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal class GetTagByIdCommandHandler(IRepository repository, IMapper mapper) : ICommandHandler<GetTagByIdCommand, Result<TagResponse>>
{
    public async Task<Result<TagResponse>> Handle(GetTagByIdCommand request, CancellationToken cancellationToken)
    {
        var tag = await repository.FirstOrDefaultAsync<Core.Tag.Tag>(x => x.TagId == request.TagId);
        if (tag == null)
        {
            return TagErrors.TagNotFound;
        }
        return mapper.Map<TagResponse>(tag);
    }
}
