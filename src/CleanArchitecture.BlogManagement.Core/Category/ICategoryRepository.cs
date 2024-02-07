using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.Category;
public interface ICategoryRepository : IRepository<Category>
{
    Task<PaginatedList<Category>> GetAllCategories(int pageSize, int pageNumber, CancellationToken cancellationToken = default);

    Task<Category?>
        GetCategoriesByIds(long categoryId, CancellationToken cancellationToken = default);
}
