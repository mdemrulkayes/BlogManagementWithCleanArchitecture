using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.Tag;

public sealed class Tag : BaseAuditableEntity
{
    public long TagId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Tag(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Tag Create(string name, string description)
    {
        return new Tag(name,description);
    }
}