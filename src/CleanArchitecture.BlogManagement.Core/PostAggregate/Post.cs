using System.Collections.ObjectModel;
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
    public IEnumerable<Comment> Comments { get; private set; } = new List<Comment>();
    public IReadOnlyCollection<PostTag> PostTags => new ReadOnlyCollection<PostTag>(_postTags);
    public IReadOnlyCollection<PostCategory> PostCategories => new ReadOnlyCollection<PostCategory>(_postCategories);

    internal List<PostCategory> _postCategories = [];
    internal List<PostTag> _postTags = [];

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

    public Result<Post> AddPostCategory(Category.Category category)
    {
        var postCategory = PostCategory.Create(category, this);
        if (!postCategory.IsSuccess || postCategory.Value is null)
        {
            return postCategory.Error;
        }

        _postCategories.Add(postCategory.Value);

        return this;
    }

    public Result<Post> RemovePostCategory(long categoryId)
    {
        var postCategory = _postCategories.Find(x => x.CategoryId == categoryId);
        if (postCategory is null)
        {
            return PostErrors.PostCategoryErrors.InvalidCategory;
        }

        _postCategories.Remove(postCategory);
        return this;
    }

    public Result<Post> AddPostTag(Tag.Tag tag)
    {
        var createdPostTag = PostTag.Create(tag, this);
        if (!createdPostTag.IsSuccess || createdPostTag.Value is null)
        {
            return createdPostTag.Error;
        }

        if (_postTags.Any(x => x.TagId == tag.TagId))
        {
            return PostErrors.PostTagErrors.TagAlreadyAdded;
        }

        _postTags.Add(createdPostTag.Value);
        return this;
    }

    public Result<Post> RemovePostTag(long tagId)
    {
        var postTagDetails = _postTags.FirstOrDefault(x => x.TagId == tagId);
        if (postTagDetails is null)
        {
            return PostErrors.PostTagErrors.InvalidTag;
        }

        _postTags.Remove(postTagDetails);

        return this;
    }
}
