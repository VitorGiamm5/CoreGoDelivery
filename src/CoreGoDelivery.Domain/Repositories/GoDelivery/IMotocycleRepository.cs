using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery
{
    public interface IMotocycleRepository
    {
        Task<List<MotorcycleEntity>?> List(string? id);
        Task<MotorcycleEntity?> GetOneById(string id);
        Task<bool> CheckIsUnicById(string id);
        Task<bool> CheckIsUnicByPlateId(string id);
        Task<bool> Create(MotorcycleEntity data);
        Task<bool> DeleteById(string id);
    }
}
