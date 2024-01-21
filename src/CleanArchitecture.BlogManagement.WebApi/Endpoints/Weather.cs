using CleanArchitecture.BlogManagement.WebApi.Infrastructure;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Weather : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetWeatherForecast)
            .MapPost(CreateWeatherForeCast)
            .MapDelete(DeleteWeatherForeCast, "{id}");
    }

    private async Task<IResult> GetWeatherForecast()
    {
        return await Task.FromResult(Results.Ok());
    }

    private async Task<IResult> CreateWeatherForeCast()
    {
        return await Task.FromResult(Results.Ok());
    }

    private async Task<IResult> DeleteWeatherForeCast(long id)
    {
        return await Task.FromResult(Results.Ok());
    }
}
