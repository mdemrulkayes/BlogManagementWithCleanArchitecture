using System.Reflection;
using CleanArchitecture.BlogManagement.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.BlogManagement.Application;
public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));
        });
        return services;
    }
}
