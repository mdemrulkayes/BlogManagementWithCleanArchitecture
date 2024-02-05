namespace CleanArchitecture.BlogManagement.WebApi.Infrastructure;

public static class EndPointExtensions
{
    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null)
    {
        builder
            .MapGet(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .WithName(handler.Method.Name);
        return builder;
    }

    public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null)
    {
        builder
            .MapPost(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .WithName(handler.Method.Name);

        return builder;
    }

    public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null)
    {
        builder
            .MapPut(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .WithName(handler.Method.Name);

        return builder;
    }

    public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler,
        string pattern = "", int statusCode = StatusCodes.Status200OK, Type? responseType = null)
    {
        builder
            .MapDelete(pattern, handler)
            .Produces(statusCode, responseType)
            .ProducesValidationProblem()
            .WithName(handler.Method.Name);

        return builder;
    }
}