using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public class NotificationMotorcycleRepository : BaseRepository<NotificationMotorcycleEntity>, INotificationMotorcycleRepository
{
    public NotificationMotorcycleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public bool Create(NotificationMotorcycleEntity data)
    {
        var result = _context
            .Set<NotificationMotorcycleEntity>()
            .Add(data);

        _context.SaveChanges();

        return IsSuccessCreate(result);
    }
}
