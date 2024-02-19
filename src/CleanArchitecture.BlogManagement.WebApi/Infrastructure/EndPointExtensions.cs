using Asp.Versioning;

namespace CleanArchitecture.BlogManagement.WebApi.Infrastructure;

public static class EndPointExtensions
{
    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null, ApiVersion? versionInfo = null)
    {
        builder
            .MapGet(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .MapToApiVersion(versionInfo ?? ApiVersion.Default)
            .WithName(handler.Method.Name);
        return builder;
    }

    public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null, ApiVersion? versionInfo = null)
    {
        builder
            .MapPost(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .MapToApiVersion(versionInfo ?? ApiVersion.Default)
            .WithName(handler.Method.Name);

        return builder;
    }

    public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null, ApiVersion? versionInfo = null)
    {
        builder
            .MapPut(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .MapToApiVersion(versionInfo ?? ApiVersion.Default)
            .WithName(handler.Method.Name);

        return builder;
    }

    public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null, ApiVersion? versionInfo = null)
    {
        builder
            .MapDelete(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .MapToApiVersion(versionInfo ?? ApiVersion.Default)
            .WithName(handler.Method.Name);

        return builder;
    }
}