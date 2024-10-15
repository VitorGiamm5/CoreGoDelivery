using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Rental
{
    public class RentalService : IRentalService
    {
        public Task<ApiResponse> Create(RentalDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return Task.FromResult(apiReponse);
        }

        public Task<ApiResponse> GetOne(string id)
        {
            var results = new RentalDto()
            {
                RentalId = "locacao123",
                DayliRate = 10,
                DeliveryPersonId = "valor_diaria",
                MotorcycleId = "moto123",
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now,
                EstimatedEndDate = DateTime.Now,
                ReturnedToBaseDate = DateTime.Now,
            };

            var apiReponse = new ApiResponse()
            {
                Data = results,
                Message = null
            };

            return Task.FromResult(apiReponse);
        }

        public Task<ApiResponse> UpdateReturnedToBaseDate(string id, ReturnedToBaseDateDto data)
        {
            var results = new ResponseMessageDto()
            {
                Message = "Data de devolução informada com sucesso"
            };

            var apiReponse = new ApiResponse()
            {
                Data = results,
                Message = null
            };

            return Task.FromResult(apiReponse);
        }
    }
}
