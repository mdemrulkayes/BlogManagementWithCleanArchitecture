using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class PostTag : BaseEntity
{
    public long PostId { get; private set; }
    public Post Post { get; private set; }

    public long TagId { get; private set; }
    public Tag.Tag Tag { get; private set; }
}
