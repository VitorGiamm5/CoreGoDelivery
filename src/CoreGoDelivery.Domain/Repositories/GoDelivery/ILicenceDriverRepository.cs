using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface ILicenceDriverRepository
{
    Task<bool> Create(LicenceDriverEntity data);
    Task<bool> CheckIsUnicByLicence(string data);
    Task<bool> UpdateFileName(string id, string fileName);
}
