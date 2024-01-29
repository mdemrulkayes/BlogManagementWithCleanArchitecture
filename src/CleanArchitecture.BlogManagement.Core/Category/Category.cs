using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Core.Category;

public sealed class Category : BaseAuditableEntity
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IEnumerable<PostCategory> PostCategories { get; private set; }

    private Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<Category> Create(string name, string description)
    {
        return new Category(name, description);
    }

    public Result<Category> Update(string name, string description)
    {
        Name = name;
        Description = description;
        return this;
    }
}