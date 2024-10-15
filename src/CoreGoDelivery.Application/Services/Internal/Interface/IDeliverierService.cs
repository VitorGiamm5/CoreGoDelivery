using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Interface
{
    public interface IDeliverierService
    {
        public Task<ApiResponse> Create(DeliverierDto data);
        public Task<ApiResponse> Update(DeliverierDto data);
    }
}
