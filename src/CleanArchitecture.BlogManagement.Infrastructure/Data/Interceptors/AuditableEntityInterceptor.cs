using CleanArchitecture.BlogManagement.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Infrastructure.Data.Interceptors;
internal sealed class AuditableEntityInterceptor(ILogger<AuditableEntityInterceptor> logger,
    IIdentityService identityService,
    ITimeProvider timeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Inside auditable entity manipulation interceptor. {Date}", timeProvider.TimeNow);

        var context = eventData.Context;

        if (context is not null)
        {
            UpdateAuditableEntities(context);
        }
        logger.LogInformation("Updated auditable entities in interceptor. {Date}", timeProvider.TimeNow);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext context)
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
                    entry.Entity.CreatedDate = timeProvider.TimeNow;
                    break;
                }
                case EntityState.Modified:
                {
                    entry.Entity.UpdatedBy = identityService.UserId;
                    entry.Entity.UpdatedDate = timeProvider.TimeNow;
                    break;
                }
            }
        }
    }
}
