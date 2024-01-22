﻿using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using FluentValidation;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Create;
internal sealed class CreateTagCommandHandler(IRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<CreateTagCommand, Result<TagResponse>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<TagResponse>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tagAlreadyExists = await repository.AnyAsync<TagCore>(x =>
            x.Name.ToLower() == request.Name.ToLower());

        if (tagAlreadyExists)
        {
            return TagErrors.TagNameAlreadyExists;
        }

        var tag = TagCore.Create(request.Name, request.Description);

        await repository.Add(tag);
        await unitOfWork.CommitAsync(cancellationToken);

        return mapper.Map<TagCore, TagResponse>(tag);
    }
}
