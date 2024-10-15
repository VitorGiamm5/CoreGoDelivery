using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Interface
{
    public interface IDeliverierService
    {
        Task<ApiResponse> Create(DeliverierDto data);
        Task<ApiResponse> UploadCnh(string id);
    }
}
