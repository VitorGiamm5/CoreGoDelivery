using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery
{
    public interface IMotocycleRepository
    {
        Task<List<MotocycleEntity>?> List(string? id);
        Task<MotocycleEntity?> GetOneById(string id);
        Task<bool> CheckIsUnicById(string id);
        Task<bool> CheckIsUnicByPlateId(string id);
        Task<bool> Create(MotocycleEntity data);
        Task<bool> DeleteById(string id);
    }
}
