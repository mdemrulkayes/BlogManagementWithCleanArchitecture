using System.Linq.Expressions;

namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IRepository
{
    Task<TEntity> Add<TEntity>(TEntity entity) 
        where TEntity : BaseEntity;
    Task<TEntity> Update<TEntity>(TEntity entity) 
        where TEntity : BaseEntity;
    Task Delete<TEntity>(TEntity entity)
        where TEntity : BaseEntity;
    Task Delete<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity: BaseEntity;
    Task<TEntity?> FirstOrDefaultAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression,
        string includeProperties = ""
        ) 
        where TEntity : BaseEntity;
    IQueryable<TEntity> FindQueryable<TEntity>(
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
        ) 
        where TEntity : BaseEntity;
    Task<List<TEntity>> FindAsync<TEntity>(
        Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default
        ) 
        where TEntity : class;
    Task<List<TEntity>> FindAllAsync<TEntity>(
        CancellationToken cancellationToken = default
        ) 
        where TEntity : BaseEntity;
    Task<bool> AnyAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression
        )
        where TEntity : BaseEntity;
}
