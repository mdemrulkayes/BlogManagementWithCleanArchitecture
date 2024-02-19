using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using CleanArchitecture.BlogManagement.WebApi.Filters;

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

    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo {Title = "Blog Management API", Version = "v1"});
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });

            opt.OperationFilter<AddApiVersionHeaderFilter>();
        });

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
