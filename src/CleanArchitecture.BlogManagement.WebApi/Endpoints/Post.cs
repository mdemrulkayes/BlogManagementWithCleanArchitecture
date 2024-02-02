using CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
using CleanArchitecture.BlogManagement.Application.Post.AddPostTag;
using CleanArchitecture.BlogManagement.Application.Post.Create;
using CleanArchitecture.BlogManagement.Application.Post.CreateComment;
using CleanArchitecture.BlogManagement.Application.Post.Delete;
using CleanArchitecture.BlogManagement.Application.Post.DeleteComment;
using CleanArchitecture.BlogManagement.Application.Post.DeletePostCategory;
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
        builder.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetAllPublishedPost)
            .MapGet(GetPostById, "{postId}")
            .MapPost(CreatePost)
            .MapPut(UpdatePost, "{postId}")
            .MapPut(UpdatePostStatus,"{postId}/status/change")
            .MapDelete(DeletePost, "{postId}")
            .MapPost(AddComment, "{postId}/comment")
            .MapPut(UpdateComment, "{postId}/comment/{commentId}")
            .MapDelete(DeleteComment, "{postId}/comment/{commentId}")
            .MapPut(AddCategory, "{postId}/category/add")
            .MapDelete(RemoveCategory, "{postId}/category/remove/{categoryId}")
            .MapPut(AddPostTag, "{postId}/tag/add");
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

    private static async Task<IResult> GetAllPublishedPost(ISender sender, [AsParameters]GetAllPostQuery query)
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
}
