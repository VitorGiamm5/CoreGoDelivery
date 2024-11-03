using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;

namespace CoreGoDelivery.Domain.Repositories.GoDelivery;

public interface IDeliverierRepository
{
    Task<bool> CheckIsUnicById(string id);
    Task<bool> CheckIsUnicByCnpj(string id);
    Task<bool> Create(DeliverierEntity data);
    Task<DeliverierEntity?> GetOneById(string id);
    Task<DeliverierEntity?> GetOneByIdLicense(string id);
}
