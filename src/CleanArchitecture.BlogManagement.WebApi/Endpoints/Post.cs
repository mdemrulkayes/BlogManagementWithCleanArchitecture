using CleanArchitecture.BlogManagement.Application.Post;
using CleanArchitecture.BlogManagement.Application.Post.Create;
using CleanArchitecture.BlogManagement.Core.Base;
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
            .MapPost(CreatePost);
    }

    private async Task<IResult> CreatePost(ISender sender, CreatePostCommand command)
    {
        Result<PostResponse> createdPost = await sender.Send(command);
        return createdPost.IsSuccess ? Results.Ok(createdPost.Value) : createdPost.ConvertToProblemDetails();
    }
}
