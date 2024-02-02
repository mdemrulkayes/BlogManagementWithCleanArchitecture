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
                    Comments: context.Mapper.Map<List<CommentResponse>>(post.Comments),
                    Categories: context.Mapper.Map<List<PostCategoryResponse>>(post.PostCategories),
                    Tags: context.Mapper.Map<List<PostTagResponse>>(post.PostTags)
                ))
            .ReverseMap();

        CreateMap<Comment, CommentResponse>()
            .ConstructUsing(x =>
                new CommentResponse(x.CommentId, x.Text)
            )
            .ReverseMap();

        CreateMap<PostCategory, PostCategoryResponse>()
            .ConstructUsing(x => 
                new PostCategoryResponse(x.CategoryId, x.Category.Name)
            )
            .ReverseMap();

        CreateMap<PostTag, PostTagResponse>()
            .ConstructUsing(x =>
                new PostTagResponse(x.TagId, x.Tag.Name)
            )
            .ReverseMap();
    }
}
