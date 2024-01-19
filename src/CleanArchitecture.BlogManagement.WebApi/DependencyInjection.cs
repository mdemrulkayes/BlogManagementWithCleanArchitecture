﻿using CleanArchitecture.BlogManagement.WebApi.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;

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
        });

        return services;
    }

    public static void MapEndpoints(this WebApplication app)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var classes = assemblies.Distinct().SelectMany(x => x.GetTypes())
            .Where(x => typeof(EndpointGroupBase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        foreach (var classe in classes)
        {
            var instance = Activator.CreateInstance(classe) as EndpointGroupBase;
            instance?.Map(app);
        }
    }
}
