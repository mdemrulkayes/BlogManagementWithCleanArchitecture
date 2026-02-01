using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using Serilog;
using System.Reflection;

namespace CleanArchitecture.BlogManagement.WebApi;

public static class DependencyInjection
{
    public static ConfigureHostBuilder RegisterSerilog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        });
        return builder;
    }

    public static IServiceCollection RegisterOpenApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName.ToLower()}")
            .WithTags(groupName)
            .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app);
            }
        }

        return app;
    }
}
