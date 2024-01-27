using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.WebApi.Extensions;

namespace CleanArchitecture.BlogManagement.WebApi.Infrastructure;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication builder);

    public static IResult ReturnResultValue<T>(Result<T> result)
    {
        return result.IsSuccess ? Results.Ok(result.Value) : result.ConvertToProblemDetails();
    }

    public static IResult TwoValuesAreNotSameReturnBadRequest(object firstOne, object secondOne)
    {
        return Results.BadRequest("Invalid operation");
    }
}
