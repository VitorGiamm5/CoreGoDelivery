using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface INotificationMotorcycleRepository
{
    Task<bool> CreateAsync(NotificationMotorcycleEntity data);
}
