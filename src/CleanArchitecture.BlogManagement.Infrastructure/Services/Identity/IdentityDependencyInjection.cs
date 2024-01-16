using CleanArchitecture.BlogManagement.Core.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.BlogManagement.Infrastructure.Services.Identity;
internal static class IdentityDependencyInjection
{
    internal static IServiceCollection RegisterIdentityServices(this IServiceCollection service)
    {
        return service.AddScoped<IIdentityService, IdentityService>();
    }
}
