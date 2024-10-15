using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery
{
    public interface ILicenceDriverRepository
    {
        Task<bool> Create(LicenceDriverEntity data);
    }
}
