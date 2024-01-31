using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class PostTag : BaseEntity
{
    public long PostId { get; private set; }
    public Post Post { get; private set; }

    public long TagId { get; private set; }
    public Tag.Tag Tag { get; private set; }

    public DateTimeOffset DateAdded { get; private set; }

    internal PostTag()
    {
        
    }
    internal PostTag(Tag.Tag tag, Post post)
    {
        Post = post;
        Tag = tag;
        DateAdded = DateTimeOffset.Now;
    }
    public static Result<PostTag> Create(Tag.Tag tag, Post post)
    {
        return new PostTag(tag, post);
    }
}
