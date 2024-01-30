using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Infrastructure.Data;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence;
internal class UnitOfWork(BlogDbContext dbContext) : IUnitOfWork
{
    private bool _disposed;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                dbContext.Dispose();
        _disposed = true;
    }
}
