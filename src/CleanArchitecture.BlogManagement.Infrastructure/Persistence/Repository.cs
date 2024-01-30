using System.Linq.Expressions;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence;
internal class Repository<TEntity>(BlogDbContext dbContext) 
    : IRepository<TEntity> where TEntity : BaseEntity
{
    public async Task<TEntity> Add(TEntity entity)
    {
        var addedEntity = dbContext
            .Set<TEntity>()
            .Add(entity)
            .Entity;
        return await Task
            .FromResult(addedEntity);
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        var updatedEntity = dbContext
            .Set<TEntity>()
            .Update(entity)
            .Entity;
        return await Task
            .FromResult(updatedEntity);
    }

    public async Task Delete(TEntity entity) 
    {
        dbContext
            .Set<TEntity>()
            .Remove(entity);
        await Task
            .CompletedTask;
    }

    public async Task Delete(Expression<Func<TEntity, bool>> expression)
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

    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression,
        string includeProperties = ""
        )
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

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(expression);
    }
}
