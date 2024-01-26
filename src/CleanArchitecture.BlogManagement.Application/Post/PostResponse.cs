using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post;

public sealed record PostResponse(long PostId, string Title, string Slug, PostStatus Status, string Text, List<CommentResponse> Comments);
