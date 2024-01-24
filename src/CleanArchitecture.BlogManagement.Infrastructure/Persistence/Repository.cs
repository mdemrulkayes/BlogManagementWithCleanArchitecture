using System.Linq.Expressions;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence;
internal class Repository(BlogDbContext dbContext) : IRepository
{
    public async Task<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : BaseEntity
    {
        var addedEntity = dbContext
            .Set<TEntity>()
            .Add(entity)
            .Entity;
        return await Task
            .FromResult(addedEntity);
    }

    public async Task<TEntity> Update<TEntity>(TEntity entity)
        where TEntity : BaseEntity
    {
        var updatedEntity = dbContext
            .Set<TEntity>()
            .Update(entity)
            .Entity;
        return await Task
            .FromResult(updatedEntity);
    }

    public async Task Delete<TEntity>(TEntity entity) 
        where TEntity : BaseEntity
    {
        dbContext
            .Set<TEntity>()
            .Remove(entity);
        await Task
            .CompletedTask;
    }

    public async Task Delete<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : BaseEntity
    {
        var data = await dbContext
            .Set<TEntity>()
            .Where(expression)
            .ToListAsync();

        dbContext
            .Set<TEntity>()
            .RemoveRange(data);

        await Task
            .CompletedTask;
    }

    public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression,
        string includeProperties = ""
        )
        where TEntity : BaseEntity
    {
        var query = dbContext
            .Set<TEntity>()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty)
                => current.Include(includeProperty));
        }

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(expression);
    }

    public IQueryable<TEntity> FindQueryable<TEntity>(
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
        ) 
        where TEntity : BaseEntity
    {
        var query = dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(expression);

        return orderBy != null 
            ? orderBy(query) 
            : query;
    }

    public async Task<List<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>>? expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default) where TEntity : class
    {
        var query = expression != null
            ? dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(expression)
            : dbContext
                .Set<TEntity>()
                .AsNoTracking();

        return orderBy != null
            ? await orderBy(query)
                .ToListAsync(cancellationToken)
            : await query
                .ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> FindAllAsync<TEntity>(CancellationToken cancellationToken) where TEntity : BaseEntity
    {
        return await dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity
    {
        return await dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(expression);
    }
}
