using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierService : IDeliverierService
    {
        public async Task<ApiResponse> Create(DeliverierDto data)
        {
            var result = new DeliverierDto()
            {
                DeliverierId = "entregador123",
                FullName = "João da Silva",
                BirthDate = DateTime.Now.AddYears(-20),
                LicenseNumber = "12345678900",
                LicenseType = data.LicenseType,
                LicenseImageBase64 = "base64string"
            };

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return apiReponse;
        }

        public async Task<ApiResponse> UploadCnh(string id)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return apiReponse;
        }
    }
}
