using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag;
internal struct TagErrors
{
    public static Error TagNameAlreadyExists => Error.Failure("Tag.TagNameAlreadyExists", "Tag name is already exists.");
}
