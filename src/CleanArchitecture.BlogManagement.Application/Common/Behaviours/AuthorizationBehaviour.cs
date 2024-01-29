using System.Reflection;
using System.Security.Claims;
using CleanArchitecture.BlogManagement.Application.Common.Security;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BlogManagement.Application.Common.Behaviours;
internal sealed class AuthorizationBehaviour<TRequest, TResponse>(
    ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger,
    IIdentityService identityService,
    IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Inside authorization attribute to check valid user");

        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
        if (authorizeAttributes.Any())
        {
            var unauthorizedError = (dynamic)Error.Unauthorized("Unauthorized", "User is not authorized to access the resource");

            var userId = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return unauthorizedError;
            }

            if (!await identityService.IsValidUser())
            {
                return unauthorizedError;
            }

        }

        return await next();
    }
}
