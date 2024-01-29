namespace CleanArchitecture.BlogManagement.Core.Identity;
public interface IIdentityService
{
    public Guid UserId { get; }

    public Task<bool> IsValidUser();
}
