using CleanArchitecture.BlogManagement.Infrastructure.Data;
using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.BlogManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("BlogDbContext"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
