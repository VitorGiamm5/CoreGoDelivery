using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public abstract class BaseRepository<T> : IBaseRepository where T : class
    {
        public readonly AplicationDbContext _context;

        protected BaseRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public static bool IsSuccessCreate(EntityEntry<T> result)
        {
            var success = result.State == EntityState.Added || result.State == EntityState.Unchanged;

            return success;
        }

        public static bool Ok(object? result)
        {
            var success = result != null;

            return success;
        }
    }
}
