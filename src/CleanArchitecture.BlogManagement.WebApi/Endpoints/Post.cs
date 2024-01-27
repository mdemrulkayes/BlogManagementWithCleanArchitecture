using CleanArchitecture.BlogManagement.Application.Post.Create;
using CleanArchitecture.BlogManagement.Application.Post.CreateComment;
using CleanArchitecture.BlogManagement.Application.Post.DeleteComment;
using CleanArchitecture.BlogManagement.Application.Post.Query;
using CleanArchitecture.BlogManagement.Application.Post.UpdateComment;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using MediatR;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public class Post : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetPostById, "{postId}")
            .MapPost(CreatePost)
            .MapPost(AddComment, "{postId}/comment")
            .MapPost(UpdateComment, "{postId}/comment/{commentId}")
            .MapDelete(DeleteComment, "{postId}/comment/{commentId}");
    }

    private static async Task<IResult> CreatePost(ISender sender, CreatePostCommand command)
    {
        var createdPost = await sender.Send(command);
        return ReturnResultValue(createdPost);
    }

    private static async Task<IResult> GetPostById(ISender sender, long postId)
    {
        var result = await sender.Send(new GetPostByIdQuery(postId));
        return ReturnResultValue(result);
    }

    private static async Task<IResult> AddComment(ISender sender, long postId, CreateCommentCommand command)
    {
        if (command.PostId != postId)
        {
            return Results.BadRequest("Invalid request");
        }

        var result = await sender.Send(command);

        return ReturnResultValue(result);
    }

    private static async Task<IResult> UpdateComment(ISender sender, long postId, long commentId, UpdateCommentCommand command)
    {
        if (command.PostId != postId || command.CommentId != commentId)
        {
            return Results.BadRequest("Invalid request");
        }

        var result = await sender.Send(command);
        return ReturnResultValue(result);
    }

    private static async Task<IResult> DeleteComment(ISender sender, long postId, long commentId)
    {
        var result = await sender.Send(new DeleteCommentCommand(postId, commentId));
        return ReturnResultValue(result);
    }
}
