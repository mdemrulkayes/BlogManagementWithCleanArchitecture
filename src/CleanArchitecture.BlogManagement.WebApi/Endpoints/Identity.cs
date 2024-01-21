using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using CleanArchitecture.BlogManagement.WebApi.Infrastructure;

namespace CleanArchitecture.BlogManagement.WebApi.Endpoints;

public sealed class Identity : EndpointGroupBase
{
    public override void Map(WebApplication builder)
    {
        builder.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
