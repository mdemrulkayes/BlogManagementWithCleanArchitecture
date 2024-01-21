using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.Category;

public sealed class Category : BaseAuditableEntity
{
    public string CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Category Create(string name, string description)
    {
        return new Category(name, description);
    }
}