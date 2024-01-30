namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
