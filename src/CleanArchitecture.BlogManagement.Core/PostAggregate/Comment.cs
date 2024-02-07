using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class Comment : BaseAuditableEntity
{
    public long CommentId { get; private set; }
    public string Text { get; private set; }
    public bool IsDeleted { get; private set; }

    public long PostId { get; private set; }

    private Comment(string text, long postId)
    {
        Text = text;
        PostId = postId;
    }

    public static Result<Comment> CreateComment(string text, long postId)
    {
        return new Comment(text, postId);
    }

    public Result<Comment> Update(string text)
    {
        Text = text;
        return this;
    }

    public Result<Comment> Delete()
    {
        IsDeleted = true;
        return this;
    }

}
