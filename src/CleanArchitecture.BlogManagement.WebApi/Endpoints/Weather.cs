using CleanArchitecture.BlogManagement.WebApi.Infrastructure;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public class Weather : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup("weather")
            .MapGet("/",  async () => await GetWeatherForecast())
            .WithName("Get Weather")
            .RequireAuthorization()
            .WithOpenApi();

    }

    private async Task<IResult> GetWeatherForecast()
    {
        return await Task.FromResult(Results.Ok());
    }
}
