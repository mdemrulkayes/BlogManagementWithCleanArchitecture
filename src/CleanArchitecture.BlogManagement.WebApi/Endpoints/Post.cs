using CleanArchitecture.BlogManagement.Application.Post.Create;
using CleanArchitecture.BlogManagement.Application.Post.CreateComment;
using CleanArchitecture.BlogManagement.Application.Post.Query;
using CleanArchitecture.BlogManagement.WebApi.Extensions;
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
            .MapPost(AddComment, "{postId}/comment");
    }

    private static async Task<IResult> CreatePost(ISender sender, CreatePostCommand command)
    {
        var createdPost = await sender.Send(command);
        return createdPost.IsSuccess ? Results.Ok(createdPost.Value) : createdPost.ConvertToProblemDetails();
    }

    private static async Task<IResult> GetPostById(ISender sender, long postId)
    {
        var result = await sender.Send(new GetPostByIdQuery(postId));
        return result.IsSuccess ? Results.Ok(result.Value) : result.ConvertToProblemDetails();
    }

    private static async Task<IResult> AddComment(ISender sender, long postId, CreateCommentCommand command)
    {
        if (command.PostId != postId)
        {
            return Results.BadRequest("Invalid operation");
        }

        var result = await sender.Send(command);

        return result.IsSuccess ? Results.Ok(result.Value) : result.ConvertToProblemDetails();
    }
}
