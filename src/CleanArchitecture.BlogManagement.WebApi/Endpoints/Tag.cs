using CleanArchitecture.BlogManagement.Application.Tag;
using CleanArchitecture.BlogManagement.Application.Tag.Create;
using CleanArchitecture.BlogManagement.Application.Tag.Delete;
using CleanArchitecture.BlogManagement.Application.Tag.Query;
using CleanArchitecture.BlogManagement.Application.Tag.Update;
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
            //.RequireAuthorization()
            .MapGet(GetAllTags)
            .MapGet(GetTagDetailsById, "{tagId}")
            .MapPost(CreateTag)
            .MapPut(UpdateTag, "{tagId}")
            .MapDelete(DeleteTag, "{tagId}");
    }

    private async Task<IResult> GetAllTags(ISender sender)
    {
        Result<List<TagResponse>> tags = await sender.Send(new GetAllTagsQuery());
        return tags.IsSuccess ? Results.Ok(tags.Value) : tags.ConvertToProblemDetails();
    }

    private async Task<IResult> GetTagDetailsById(ISender sender, long tagId)
    {
        Result<TagResponse> tag = await sender.Send(new GetTagByIdQuery(tagId));
        return tag.IsSuccess ? Results.Ok(tag.Value) : tag.ConvertToProblemDetails();
    }

    private async Task<IResult> CreateTag(ISender sender, CreateTagCommand command)
    {
        Result<TagResponse> createdTag = await sender.Send(command);

        return createdTag.IsSuccess ? Results.Ok(value: createdTag.Value) : createdTag.ConvertToProblemDetails();
    }

    private async Task<IResult> UpdateTag(ISender sender, long tagId, UpdateTagCommand command)
    {
        if (tagId != command.TagId)
        {
            return Results.BadRequest("Invalid request");
        }
        Result<TagResponse> updatedTag = await sender.Send(command);
        return updatedTag.IsSuccess ? Results.Ok(updatedTag.Value) : updatedTag.ConvertToProblemDetails();
    }

    private async Task<IResult> DeleteTag(ISender sender, long tagId)
    {
        Result<bool> deleteTag = await sender.Send(new DeleteTagCommand(tagId));

        return deleteTag.IsSuccess ? Results.Ok(deleteTag.Value) : deleteTag.ConvertToProblemDetails();
    }
}
