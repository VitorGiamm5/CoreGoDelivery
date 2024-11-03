using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public abstract class BaseRepository<T> : IBaseRepository where T : class
{
    public readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public static bool IsSuccessCreate(EntityEntry<T> result)
    {
        var success = result.State == EntityState.Added || result.State == EntityState.Unchanged;

        return success;
    }

    public static bool IsUnic(object? result)
    {
        var success = result == null;

        return success;
    }

    public static bool HasValue(object? result)
    {
        var success = result != null;

        return success;
    }
}
