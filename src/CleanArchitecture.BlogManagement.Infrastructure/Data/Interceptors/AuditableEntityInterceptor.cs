using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BlogManagement.Infrastructure.Data.Interceptors;
internal sealed class AuditableEntityInterceptor(ILogger<AuditableEntityInterceptor> logger, IIdentityService identityService) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var time = DateTimeOffset.UtcNow;
        logger.LogInformation("Inside auditable entity manipulation interceptor. {Date}", time);

        var context = eventData.Context;

        if (context is not null)
        {
            var trackedEntries = context.ChangeTracker.Entries<BaseAuditableEntity>()
                .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();
            foreach (var entry in trackedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    {
                        entry.Entity.CreatedBy = identityService.UserId;
                        entry.Entity.CreatedDate = time;
                            break;
                        }
                    case EntityState.Modified:
                    {
                        entry.Entity.UpdatedBy = identityService.UserId;
                        entry.Entity.UpdatedDate = time;
                        break;
                    }
                }
            }
        }
        logger.LogInformation("Updated auditable entities in interceptor. {Date}", time);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
