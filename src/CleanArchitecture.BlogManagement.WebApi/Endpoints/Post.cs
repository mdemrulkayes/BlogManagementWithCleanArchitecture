using Asp.Versioning;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Application.Post;
using CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
using CleanArchitecture.BlogManagement.Application.Post.AddPostTag;
using CleanArchitecture.BlogManagement.Application.Post.Create;
using CleanArchitecture.BlogManagement.Application.Post.CreateComment;
using CleanArchitecture.BlogManagement.Application.Post.Delete;
using CleanArchitecture.BlogManagement.Application.Post.DeleteComment;
using CleanArchitecture.BlogManagement.Application.Post.DeletePostCategory;
using CleanArchitecture.BlogManagement.Application.Post.DeletePostTag;
using CleanArchitecture.BlogManagement.Application.Post.Query;
using CleanArchitecture.BlogManagement.Application.Post.Update;
using CleanArchitecture.BlogManagement.Application.Post.UpdateComment;
using CleanArchitecture.BlogManagement.Application.Post.UpdatePostStatus;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public class Post : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(ApiVersion.Default)
            .Build();

        builder.MapGroup(this)
            .WithApiVersionSet(apiVersionSet)
            //.RequireAuthorization()
            .MapGet(GetAllPublishedPost, responseType:typeof(PagedListDto<PostResponse>))
            .MapGet(GetPostById, "{postId}", responseType: typeof(PostResponse))
            .MapPost(CreatePost, responseType: typeof(long))
            .MapPut(UpdatePost, "{postId}", responseType: typeof(long))
            .MapPut(UpdatePostStatus, "{postId}/status/change", responseType: typeof(long))
            .MapDelete(DeletePost, "{postId}", responseType: typeof(bool))
            .MapPost(AddComment, "{postId}/comment", responseType: typeof(long))
            .MapPut(UpdateComment, "{postId}/comment/{commentId}", responseType: typeof(long))
            .MapDelete(DeleteComment, "{postId}/comment/{commentId}", responseType: typeof(long))
            .MapPut(AddCategory, "{postId}/category/add", responseType: typeof(long))
            .MapDelete(RemoveCategory, "{postId}/category/remove/{categoryId}", responseType: typeof(long))
            .MapPut(AddPostTag, "{postId}/tag/add", responseType: typeof(long))
            .MapDelete(RemovePostTag, "{postId}/tag/remove/{tagId}", responseType: typeof(long));
    }

    private static async Task<IResult> CreatePost(ISender sender, CreatePostCommand command)
    {
        var createdPost = await sender.Send(command);
        return ReturnResultValue(createdPost);
    }

    private static async Task<IResult> UpdatePost(ISender sender, long postId, UpdatePostCommand command)
    {
        TwoValuesAreNotSameReturnBadRequest(command.PostId, postId);

        var result = await sender.Send(command);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> UpdatePostStatus(ISender sender, long postId, UpdatePostStatusCommand command)
    {
        TwoValuesAreNotSameReturnBadRequest(command.PostId, postId);

        var result = await sender.Send(command);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> GetPostById(ISender sender, long postId)
    {
        var result = await sender.Send(new GetPostByIdQuery(postId));
        return ReturnResultValue(result);
    }

    private static async Task<IResult> GetAllPublishedPost(ISender sender, GetAllPostQuery query)
    {
        var result = await sender.Send(query);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> DeletePost(ISender sender, long postId)
    {
        var result = await sender.Send(new DeletePostCommand(postId));
        return ReturnResultValue(result);
    }

    private static async Task<IResult> AddComment(ISender sender, long postId, CreateCommentCommand command)
    {
        TwoValuesAreNotSameReturnBadRequest(command.PostId, postId);

        var result = await sender.Send(command);

        return ReturnResultValue(result);
    }

    private static async Task<IResult> UpdateComment(ISender sender, long postId, long commentId, UpdateCommentCommand command)
    {
        TwoValuesAreNotSameReturnBadRequest(command.PostId, postId);

        TwoValuesAreNotSameReturnBadRequest(command.CommentId, commentId);

        var result = await sender.Send(command);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> DeleteComment(ISender sender, long postId, long commentId)
    {
        var result = await sender.Send(new DeleteCommentCommand(postId, commentId));
        return ReturnResultValue(result);
    }

    private static async Task<IResult> AddCategory(ISender sender, long postId, AddPostCategoryCommand command)
    {
        TwoValuesAreNotSameReturnBadRequest(postId, command.PostId);
        var result = await sender.Send(command);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> RemoveCategory(ISender sender, long postId, long categoryId)
    {
        var result = await sender.Send(new DeletePostCategoryCommand(postId, categoryId));

        return ReturnResultValue(result);
    }

    private static async Task<IResult> AddPostTag(ISender sender, long postId, AddPostTagCommand command)
    {
        TwoValuesAreNotSameReturnBadRequest(postId, command.PostId);
        var result = await sender.Send(command);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> RemovePostTag(ISender sender, long postId, long tagId)
    {
        var result = await sender.Send(new DeletePostTagCommand(postId, tagId));
        return ReturnResultValue(result);
    }

}
