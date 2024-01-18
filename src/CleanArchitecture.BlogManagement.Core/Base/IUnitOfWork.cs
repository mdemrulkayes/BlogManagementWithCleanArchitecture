namespace CleanArchitecture.BlogManagement.Core.Base;
public interface IUnitOfWork : IDisposable
{
    IRepository Repository { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
