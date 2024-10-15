using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Motocycle
{
    public class MotocycleService : IMotocycleService
    {
        public Task<ApiResponse> ChangePlate(string id, PlateIdDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return Task.FromResult(apiReponse);
        }

        public Task<ApiResponse> Create(MotocycleDto data)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return Task.FromResult(apiReponse);
        }

        public Task<ApiResponse> Delete(PlateIdDto id)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return Task.FromResult(apiReponse);
        }

        public Task<ApiResponse> GetOne(string data)
        {
            var result = new MotocycleDto()
            {
                MotocycleId = "moto123",
                YearManufacture = 2020,
                ModelName = "Mottu Sport",
                PlateId = "CDX-0101"
            };

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return Task.FromResult(apiReponse);
        }

        public Task<ApiResponse> List(PlateIdDto? data)
        {
            List<MotocycleDto> result = [
                new MotocycleDto()
                {
                    MotocycleId = "moto123",
                    YearManufacture = 2020,
                    ModelName = "Mottu Sport",
                    PlateId = "CDX-0101"
                }
            ];

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return Task.FromResult(apiReponse);
        }
    }
}
