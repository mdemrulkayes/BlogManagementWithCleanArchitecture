﻿using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
internal class GetTagByIdQueryHandler(ITagRepository repository, IMapper mapper) : ICommandHandler<GetTagByIdQuery, Result<TagResponse>>
{
    public async Task<Result<TagResponse>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await repository.FirstOrDefaultAsync(x => x.TagId == request.TagId);
        if (tag == null)
        {
            return TagErrors.TagNotFound;
        }
        return mapper.Map<TagResponse>(tag);
    }
}
