using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.Category;
public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetAllCategories(CancellationToken cancellationToken = default);

    Task<Category?>
        GetCategoriesByIds(long categoryId, CancellationToken cancellationToken = default);
}
