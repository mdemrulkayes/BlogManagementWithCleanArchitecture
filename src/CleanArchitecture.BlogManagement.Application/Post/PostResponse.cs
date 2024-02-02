using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post;

public sealed record PostResponse(
    long PostId,
    string Title,
    string Slug,
    PostStatus Status,
    string StatusText,
    string Text,
    List<CommentResponse> Comments,
    List<PostCategoryResponse> Categories,
    List<PostTagResponse> Tags);
