﻿using CleanArchitecture.BlogManagement.Application.Tag;
using CleanArchitecture.BlogManagement.Application.Tag.Create;
using CleanArchitecture.BlogManagement.Application.Tag.Query;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.WebApi.Extensions;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Tag : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllTags)
            .MapPost(CreateTag);
    }

    private async Task<IResult> GetAllTags(ISender sender)
    {
        Result<List<TagResponse>> tags = await sender.Send(new GetTagsCommand());
        return tags.IsSuccess ? Results.Ok(tags) : tags.ConvertToProblemDetails();
    }

    private async Task<IResult> CreateTag(ISender sender, CreateTagCommand command)
    {
        Result<TagResponse> createdTag = await sender.Send(command);

        return createdTag.IsSuccess ? Results.Ok(value: createdTag.Value) : createdTag.ConvertToProblemDetails();
    }
}