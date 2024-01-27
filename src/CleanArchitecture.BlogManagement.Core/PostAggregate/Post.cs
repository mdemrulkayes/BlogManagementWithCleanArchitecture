﻿using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class Post : BaseAuditableEntity, IAggregateRoot
{
    public long PostId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public PostStatus Status { get; private set; }
    public DateTimeOffset? PublishedAt { get; private set; }
    public string Text { get; private set; }

    private readonly List<Comment> _comments = new();

    public IEnumerable<Comment> Comments => _comments.AsReadOnly();

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

    public void AddComment(string text)
    {
        var comment = Comment.CreateComment(text, PostId);
        if (!comment.IsSuccess || comment.Value is null)
        {
            return; //TODO:Need to think about it
        }
        _comments.Add(comment.Value);
    }

    public void UpdateComment(long commentId, string text)
    {
        var updateToComment = _comments.FirstOrDefault(x => x.CommentId == commentId);

        if (updateToComment is null)
        {
            return; //TODO:Need to think about it
        }

        updateToComment.Update(text);
    }

    public void DeleteComment(long commentId)
    {
        var deleteToComment = _comments.FirstOrDefault(x => x.CommentId == commentId);

        if (deleteToComment is null)
        {
            return; //TODO:Need to think about it
        }
        _comments.RemoveAll(x => x.CommentId == commentId);
    }
}
