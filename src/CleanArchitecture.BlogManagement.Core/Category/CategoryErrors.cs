using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.Category;
public struct CategoryErrors
{
    public static Error NotFound => Error.NotFound("Category.NotFound", "Category details not found");
}
