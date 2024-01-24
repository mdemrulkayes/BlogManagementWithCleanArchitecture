using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Category;
internal struct CategoryErrors
{
    public static Error NotFound => Error.NotFound("Category.NotFound", "Category details not found");
}
