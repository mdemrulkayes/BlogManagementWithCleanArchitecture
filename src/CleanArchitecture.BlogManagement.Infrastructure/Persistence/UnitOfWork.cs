using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence;
internal class UnitOfWork : IUnitOfWork
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IRepository Repository()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
