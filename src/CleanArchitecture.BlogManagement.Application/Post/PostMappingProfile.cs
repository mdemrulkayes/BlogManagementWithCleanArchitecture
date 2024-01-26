using AutoMapper;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post;
internal sealed class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Core.PostAggregate.Post, PostResponse>()
            .ConstructUsing(
                (post, context) => new PostResponse(
                    PostId: post.PostId,
                    Title: post.Title,
                    Slug: post.Slug,
                    Status: post.Status,
                    StatusText: post.Status.ToString(),
                    Text: post.Text,
                    Comments: context.Mapper.Map<List<CommentResponse>>(post.Comments)
                ))
            .ReverseMap();

        CreateMap<Comment, CommentResponse>()
            .ConstructUsing(x =>
                new CommentResponse(x.CommentId, x.Text)
            )
            .ReverseMap();
    }
}
