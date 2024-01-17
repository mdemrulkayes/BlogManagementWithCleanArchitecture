namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IUnitOfWork : IDisposable
{
    IRepository Repository();
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
