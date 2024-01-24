using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public class Comment : BaseAuditableEntity
{
    public long CommentId { get; private set; }
    public string Text { get; private set; }
    public bool IsDeleted { get; private set; }

    public long PostId { get; private set; }

    internal Comment Update(string text)
    {
        Text = text;
        return this;
    }
}
