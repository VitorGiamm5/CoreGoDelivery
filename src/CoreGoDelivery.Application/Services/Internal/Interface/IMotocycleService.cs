using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Interface
{
    public interface IMotocycleService
    {
        Task<ApiResponse> Create(MotocycleDto data);
        Task<ApiResponse> List(string? data);
        Task<ApiResponse> GetOne(string data);
        Task<ApiResponse> ChangePlate(string id, PlateIdDto data);
        Task<ApiResponse> Delete(string id);
    }
}
