using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public abstract class BaseRepository<T> : IBaseRepository where T : class
    {
        public readonly AplicationDbContext _context;

        protected BaseRepository(AplicationDbContext context)
        {
            _context = context;
        }
    }
}
