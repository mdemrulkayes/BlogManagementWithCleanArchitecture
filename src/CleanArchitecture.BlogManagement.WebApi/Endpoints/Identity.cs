using Asp.Versioning;
using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Identity : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(ApiVersion.Default)
            .Build();

        builder.MapGroup(this)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersion.Default)
            .MapIdentityApi<ApplicationUser>();
    }
}
