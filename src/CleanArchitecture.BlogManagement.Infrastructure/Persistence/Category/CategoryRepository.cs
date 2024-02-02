using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;
using CleanArchitecture.BlogManagement.Core.Extensions;
using CleanArchitecture.BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CategoryCore = CleanArchitecture.BlogManagement.Core.Category.Category;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence.Category;
internal sealed class CategoryRepository(BlogDbContext dbContext)
    : Repository<CategoryCore>(dbContext), ICategoryRepository
{
    private readonly BlogDbContext _dbContext = dbContext;

    public async Task<PaginatedList<CategoryCore>> GetAllCategories(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Categories
            .ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<CategoryCore?> GetCategoriesByIds(long categoryId, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Categories
            .FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
    }
}
