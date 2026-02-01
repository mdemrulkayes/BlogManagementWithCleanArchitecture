using Asp.Versioning;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Application.Tag;
using CleanArchitecture.BlogManagement.Application.Tag.Create;
using CleanArchitecture.BlogManagement.Application.Tag.Delete;
using CleanArchitecture.BlogManagement.Application.Tag.Query;
using CleanArchitecture.BlogManagement.Application.Tag.Update;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Tag : EndpointGroupBase
{
    public ApiVersion V1 = ApiVersion.Default;
    public ApiVersion V2 = new(2);

    public override void Map(WebApplication builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(V1)
            .HasApiVersion(V2)
            .Build();

        builder.MapGroup(this)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(V1)
            //.RequireAuthorization()
            .MapGet(GetAllTags, responseType: typeof(PagedListDto<TagResponse>), versionInfo: V1)
            .MapGet(GetTagDetailsById, "{tagId}", responseType: typeof(TagResponse), versionInfo: V1)
            .MapPost(CreateTag, responseType: typeof(TagResponse), versionInfo: V2)
            .MapPut(UpdateTag, "{tagId}", responseType: typeof(TagResponse), versionInfo: V1)
            .MapDelete(DeleteTag, "{tagId}", responseType: typeof(bool), versionInfo: V1);
    }

    private static async Task<IResult> GetAllTags(ISender sender, int pageNumber = 1, int pageSize = 10)
    {
        var queryParams = new GetAllTagsQuery(pageNumber, pageSize);
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
