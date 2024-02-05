﻿using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Application.Tag;
using CleanArchitecture.BlogManagement.Application.Tag.Create;
using CleanArchitecture.BlogManagement.Application.Tag.Delete;
using CleanArchitecture.BlogManagement.Application.Tag.Query;
using CleanArchitecture.BlogManagement.Application.Tag.Update;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Tag : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetAllTags, responseType: typeof(PagedListDto<TagResponse>))
            .MapGet(GetTagDetailsById, "{tagId}", responseType: typeof(TagResponse))
            .MapPost(CreateTag, responseType: typeof(TagResponse))
            .MapPut(UpdateTag, "{tagId}", responseType: typeof(TagResponse))
            .MapDelete(DeleteTag, "{tagId}", responseType: typeof(bool));
    }

    private static async Task<IResult> GetAllTags(ISender sender, [AsParameters]GetAllTagsQuery queryParams)
    {
        var tags = await sender.Send(queryParams);
        return ReturnResultValue(tags);
    }

    private static async Task<IResult> GetTagDetailsById(ISender sender, long tagId)
    {
        var tag = await sender.Send(new GetTagByIdQuery(tagId));
        return ReturnResultValue(tag);
    }

    private static async Task<IResult> CreateTag(ISender sender, CreateTagCommand command)
    {
        var createdTag = await sender.Send(command);

        return ReturnResultValue(createdTag);
    }

    private static async Task<IResult> UpdateTag(ISender sender, long tagId, UpdateTagCommand command)
    {
        if (tagId != command.TagId)
        {
            return Results.BadRequest("Invalid request");
        }
        var updatedTag = await sender.Send(command);
        return ReturnResultValue(updatedTag);
    }

    private static async Task<IResult> DeleteTag(ISender sender, long tagId)
    {
        var deleteTag = await sender.Send(new DeleteTagCommand(tagId));

        return ReturnResultValue(deleteTag);
    }
}
