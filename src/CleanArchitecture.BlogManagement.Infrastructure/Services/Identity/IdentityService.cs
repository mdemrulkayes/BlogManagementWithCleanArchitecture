using CleanArchitecture.BlogManagement.Core.Identity;

namespace CleanArchitecture.BlogManagement.Infrastructure.Services.Identity;
internal sealed class IdentityService : IIdentityService
{
    public Guid UserId => Guid.NewGuid();
}
