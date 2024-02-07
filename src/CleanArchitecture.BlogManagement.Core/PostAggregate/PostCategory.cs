using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate;
public sealed class PostCategory : BaseEntity
{
    public long CategoryId { get; private set; }
    public Category.Category Category { get; private set; }
    public long PostId { get; private set; }
    public Post Post { get; private set; }
    public DateTimeOffset DateAdded { get; private set; }

    private PostCategory()
    {
        
    }
    private PostCategory(Category.Category category, Post post)
    {
        Category = category;
        Post = post;
        DateAdded = DateTimeOffset.Now;
    }

    public static Result<PostCategory> Create(Category.Category? category, Post post)
    {
        if (category is null)
        {
            return PostErrors.PostCategoryErrors.InvalidCategory;
        }
        return new PostCategory(category, post);
    }
}
