using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag;
internal struct TagErrors
{
    public static Error TagNotFound => Error.NotFound("Tag.TagNotFound", "Tag not found.");
}
