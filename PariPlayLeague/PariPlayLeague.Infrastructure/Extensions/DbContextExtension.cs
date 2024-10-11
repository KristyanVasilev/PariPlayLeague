using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Domain.Abstractions.Interfaces;

namespace PariPlayLeague.Infrastructure.Extensions
{
    public static class DbContextExtension
    {
        public static void UseSoftDelete(this DbContext dbContext)
        {
            dbContext.ChangeTracker.DetectChanges();

            var entitiesToDelete = dbContext
                .ChangeTracker
                .Entries()
                .Where(entry => entry.State.Equals(EntityState.Deleted));

            foreach (var entry in entitiesToDelete)
            {
                if (entry.Entity is IDeletable entity)
                {
                    entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }

        public static void UseTrackingOnCreate(this DbContext dbContext)
        {
            dbContext.ChangeTracker.DetectChanges();

            var entriesToCreate = dbContext
                .ChangeTracker
                .Entries()
                .Where(entry => entry.State.Equals(EntityState.Added));

            foreach (var entry in entriesToCreate)
            {
                if (entry.Entity is IDatable entity)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
