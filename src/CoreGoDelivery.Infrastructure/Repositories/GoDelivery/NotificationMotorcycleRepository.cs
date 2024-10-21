using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class NotificationMotorcycleRepository : BaseRepository<NotificationMotorcycleEntity>, INotificationMotorcycleRepository
    {
        public NotificationMotorcycleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> Create(NotificationMotorcycleEntity data)
        {
            var result = await _context
                .Set<NotificationMotorcycleEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return IsSuccessCreate(result);
        }
    }
}
