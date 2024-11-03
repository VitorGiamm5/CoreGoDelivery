using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface IRentalRepository
{
    Task<RentalEntity?> FindByMotorcycleId(string id);
    Task<RentalEntity?> GetByIdAsync(string id);
    Task<bool> Create(RentalEntity data);
    Task<bool> UpdateReturnedToBaseDate(string id, DateTime data);
    Task<bool> CheckMotorcycleIsAvaliable(string id);
    Task<bool> CheckisReturnedById(string id);
}
