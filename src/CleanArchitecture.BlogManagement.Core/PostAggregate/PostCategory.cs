using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class PostCategory : BaseEntity
{
    public long PostCategoryId { get; private set; }

    public long CategoryId { get; private set; }
    public Category.Category Category { get; private set; }

    public long PostId { get; private set; }
    public Post Post { get; private set; }

}
