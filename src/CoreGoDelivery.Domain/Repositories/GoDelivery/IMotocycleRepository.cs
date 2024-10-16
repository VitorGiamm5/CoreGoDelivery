using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery
{
    public interface IMotocycleRepository
    {
        Task<bool> CheckIsUnicById(string id);
        Task<bool> CheckIsUnicByPlateId(string id);
        Task<bool> Create(MotocycleEntity data);
    }
}
