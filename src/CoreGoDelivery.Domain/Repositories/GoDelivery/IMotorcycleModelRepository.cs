using CoreGoDelivery.Domain.Entities.GoDelivery.MotorcycleModel;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface IMotorcycleModelRepository
{
    Task<string> GetIdByModelName(string model);
    Task<List<MotorcycleModelEntity>> GetAll(string? name, string? id);
}
