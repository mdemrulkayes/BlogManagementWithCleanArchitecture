using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class Post : BaseAuditableEntity, IAggregateRoot
{
    public long PostId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public PostStatus Status { get; private set; }
    public DateTimeOffset? PublishedAt { get; private set; }
    public string Text { get; private set; }
    public IEnumerable<Comment> Comments { get; private set; }

    private Post(string title, string slug, string text)
    {
        Title = title;
        Slug = slug;
        Text = text;
    }

    public static Result<Post> CreatePost(string title, string slug, string text)
    {
        //Validation logics can be added here
        return new Post(title, slug, text);
    }

    public Result<Post> UpdatePost(string title, string slug, string text)
    {
        //validation logics can be added here
        Title = title;
        Slug = slug;
        Text = text;
        return this;
    }

    private void SetPostStatus(PostStatus status)
    {
        Status = status;
        PublishedAt = status == PostStatus.Published ? DateTimeOffset.UtcNow : null;
    }

    public void MarkPostAsDraft() => SetPostStatus(PostStatus.Draft);
    public void MarkPostAsPublished() => SetPostStatus(PostStatus.Published);
    public void MarkPostAsAbandoned() => SetPostStatus(PostStatus.Abandoned);
}
