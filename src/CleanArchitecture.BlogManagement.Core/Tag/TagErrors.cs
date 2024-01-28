using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.Tag;
public struct TagErrors
{
    public static Error TagNotFound => Error.NotFound("Tag.TagNotFound", "Tag not found.");
}
