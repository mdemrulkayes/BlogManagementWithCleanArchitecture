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

    public static Result<Tag> Create(string name, string description = "")
    {
        //Below validations are already checked by FluentValidation. This is an example to implement business validation here
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("Tag.Name", "Tag Name can not be empty");
        }

        if (!string.IsNullOrWhiteSpace(description) && description.Length is <= 10 or >= 150)
        {
            return Error.Validation("Tag.Description", "Tag Description can not be more than 150 characters");
        }

        return new Tag(name, description);
    }

    public Result<Tag> Update(string name, string description)
    {
        Name = name;
        Description = description;
        return this;
    }
}