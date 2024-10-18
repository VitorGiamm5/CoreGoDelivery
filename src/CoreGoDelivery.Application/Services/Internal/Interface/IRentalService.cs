using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Interface
{
    public interface IRentalService
    {
        Task<ApiResponse> GetById(string? id);
        Task<ApiResponse> Create(RentalDto data);
        Task<ApiResponse> UpdateReturnedToBaseDate(string id, ReturnedToBaseDateDto data);
        Task<bool> MotorcycleIsAvaliableValidator(string id);
    }
}
