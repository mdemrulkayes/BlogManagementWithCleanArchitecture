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
        if (string.IsNullOrWhiteSpace(tag.Name))
        {
            return PostErrors.PostTagErrors.TagNameCanNotBeEmpty;
        }

        if (!string.IsNullOrWhiteSpace(tag.Description) && tag.Description.Length <= 10)
        {
            return PostErrors.PostTagErrors.TagDescriptionLengthShouldBeMoreThan10;
        }

        return new PostTag(tag, post);
    }
}
