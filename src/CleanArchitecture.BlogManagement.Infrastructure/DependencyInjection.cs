using CleanArchitecture.BlogManagement.Infrastructure.Data;
using CleanArchitecture.BlogManagement.Infrastructure.Data.Interceptors;
using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using CleanArchitecture.BlogManagement.Infrastructure.Persistence;
using CleanArchitecture.BlogManagement.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.BlogManagement.Core.Category;
using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Core.Tag;
using CleanArchitecture.BlogManagement.Infrastructure.Persistence.Category;
using CleanArchitecture.BlogManagement.Infrastructure.Persistence.Post;
using CleanArchitecture.BlogManagement.Infrastructure.Persistence.Tag;
using SharedKernel;
using TimeProvider = CleanArchitecture.BlogManagement.Infrastructure.Services.TimeProvider;

namespace CleanArchitecture.BlogManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDatabaseInterceptors();
        services.AddDbContext<BlogDbContext>((sp,opt) =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("BlogDbContext"))
                .AddInterceptors(
                    sp.GetRequiredService<AuditableEntityInterceptor>(),
                    sp.GetRequiredService<PublishDomainEventsInterceptor>()
                    );
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        });

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddAuthorization();

        services.RegisterServices();

        services.AddTransient<ITimeProvider, TimeProvider>();

        return services;
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.RegisterIdentityServices();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterDatabaseInterceptors(this IServiceCollection services)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<PublishDomainEventsInterceptor>();
    }


    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var database = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        if (database.Database.IsSqlServer())
        {
            database.Database.Migrate();
        }
        return app;
    }
}
