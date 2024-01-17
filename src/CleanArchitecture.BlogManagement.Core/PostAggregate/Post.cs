using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class Post : BaseAuditableEntity, IAggregateRoot
{
    public long PostId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public PostStatus Status { get; private set; }
    public DateTimeOffset PublishedAt { get; private set; }
    public string Text { get; private set; }

    private readonly List<Comment> _comments = new();

    public IEnumerable<Comment> Comments => _comments.AsReadOnly();

    private Post(string title, string slug, string text, DateTimeOffset publishedAt)
    {
        Title = title;
        Slug = slug;
        Text = text;
        PublishedAt = publishedAt;
    }

    public static Post CreatePost(string title, string slug, string text)
    {
        return new Post(title, slug, text, DateTimeOffset.Now);
    }

    private void SetPostStatus(PostStatus status)
    {
        Status = status;
    }

    public void MarkPostAsDraft() => SetPostStatus(PostStatus.Draft);
    public void MarkPostAsPublished() => SetPostStatus(PostStatus.Published);
    public void MarkPostAsAbandoned() => SetPostStatus(PostStatus.Abandoned);

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }
}
