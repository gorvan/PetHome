using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Framework.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken token)
        {
            if (eventData.Context == null)
            {
                return await base.SavingChangesAsync(eventData, result, token);
            }

            var entries = eventData.Context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                if (entry.Entity is ISoftDeletable item)
                {
                    item.Delete();
                }
            }

            return await base.SavingChangesAsync(eventData, result, token);
        }
    }
}
