using System.Linq.Expressions;

namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IRepository<TEntity>  where TEntity : BaseEntity
{
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> Update(TEntity entity);

    Task Delete(TEntity entity);

    Task Delete(Expression<Func<TEntity, bool>> expression);

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
