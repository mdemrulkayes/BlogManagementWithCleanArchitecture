using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public class Comment : BaseAuditableEntity
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

    internal static Result<Comment> CreateComment(string text, long postId)
    {
        return new Comment(text, postId);
    }

    internal Comment Update(string text)
    {
        Text = text;
        return this;
    }

}
