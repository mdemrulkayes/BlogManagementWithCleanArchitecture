using CleanArchitecture.BlogManagement.Core.Base;
using MediatR;
using Microsoft.Extensions.Hosting;

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
    public IEnumerable<PostTag> PostTags { get; private set; }
    public IEnumerable<PostCategory> PostCategories { get; private set; }

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

    public void SetStatus(PostStatus status)
    {
        switch (status)
        {
            case PostStatus.Published:
                this.MarkPostAsPublished();
                break;
            case PostStatus.Abandoned:
                this.MarkPostAsAbandoned();
                break;
            case PostStatus.Draft:
            default:
                this.MarkPostAsDraft();
                break;
        }
    }

    private void SetPostStatus(PostStatus status)
    {
        Status = status;
        PublishedAt = status == PostStatus.Published ? DateTimeOffset.UtcNow : null;
    }

    private void MarkPostAsDraft() => SetPostStatus(PostStatus.Draft);
    private void MarkPostAsPublished() => SetPostStatus(PostStatus.Published);
    private void MarkPostAsAbandoned() => SetPostStatus(PostStatus.Abandoned);
}
