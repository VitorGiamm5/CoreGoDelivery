using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("entregadores")]
    [ApiController]
    public class DeliveriersController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeliverierDto request)
        {
            var result = new DeliverierDto()
            {
                DeliverierId = "entregador123",
                FullName = "João da Silva",
                BirthDate = DateTime.Now.AddYears(-20),
                LicenseNumber = "12345678900",
                LicenseType = request.LicenseType,
                LicenseImageBase64 = "base64string"
            };

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            await Task.CompletedTask;
            return Response(apiReponse);
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> Post([FromBody] LicenseImageString request)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            await Task.CompletedTask;
            return Response(apiReponse);
        }
    }
}
