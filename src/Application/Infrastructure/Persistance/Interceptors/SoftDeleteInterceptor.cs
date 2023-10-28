using Application.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Application.Infrastructure.Persistance.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public SoftDeleteInterceptor()
    {
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public static void UpdateEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        // TODO: Add logic for marking entities owned by the entity we are trying to delete as unchanged,
        // otherwise attempting to soft delete entities will throw an exception because ef core will perform
        // delete on those entities and try to update our "main" entity with NULL values in place of the owned ones
        foreach (var entry in context.ChangeTracker.Entries<ISoftDelete>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }
        }
    }
}