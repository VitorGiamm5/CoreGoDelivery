using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface ILicenceDriverRepository
{
    Task<bool> CheckIsUnicByLicence(string id);
    Task<LicenceDriverEntity?> GetOneById(string id);
    Task<bool> UpdateFileName(string id, string fileName);
}
