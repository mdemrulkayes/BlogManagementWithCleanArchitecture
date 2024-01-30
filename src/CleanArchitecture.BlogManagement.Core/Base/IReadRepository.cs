using System.Linq.Expressions;

namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IReadRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression,
        string includeProperties = ""
    );

    Task<List<TEntity>> FindAllAsync(
        Expression<Func<TEntity, bool>>? expression = null,
        CancellationToken cancellationToken = default
    );
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> expression
    );
}
