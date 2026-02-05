using System.Security.Claims;
using CleanArchitecture.BlogManagement.Core.Identity;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.BlogManagement.Infrastructure.Services.Identity;
internal sealed class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
{
    //public Guid UserId =>
    //    Guid.Parse(httpContextAccessor.HttpContext?.User?.Claims
    //                   ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.ToString() ??
    //               throw new NullReferenceException("User ID not found"));

    //public Guid UserId =>
    //    Guid.Parse(httpContextAccessor.HttpContext?.User?.Claims
    //                   ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.ToString() ??
    //               Guid.NewGuid().ToString());

    public Guid UserId => Guid.NewGuid();

    public Task<bool> IsValidUser()
    {
        //Get the user details from Identity database
        var userId = UserId;
        return Task.FromResult(true);
    }
}
