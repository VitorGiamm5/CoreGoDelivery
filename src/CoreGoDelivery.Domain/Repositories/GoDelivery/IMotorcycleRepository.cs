using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface IMotorcycleRepository
{
    Task<List<MotorcycleEntity>?> List(string plate);
    Task<MotorcycleEntity?> GetOneByIdAsync(string id);
    Task<bool> CheckIsUnicById(string id);
    Task<bool> CheckIsUnicByPlateAsync(string plate);
    Task<bool> Create(MotorcycleEntity data);
    Task<bool> DeleteById(string id);
    Task<bool> ChangePlateByIdAsync(string id, string plate);
}
