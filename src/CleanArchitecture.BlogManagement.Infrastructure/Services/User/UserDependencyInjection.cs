using CleanArchitecture.BlogManagement.Core.User;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.BlogManagement.Infrastructure.Services.User;
internal static class UserDependencyInjection
{
    internal static IServiceCollection RegisterUserServices(this IServiceCollection service)
    {
        return service.AddScoped<IUserService, UserService>();
    }
}
