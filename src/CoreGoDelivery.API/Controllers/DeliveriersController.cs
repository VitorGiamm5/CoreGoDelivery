using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("entregadores")]
    [ApiController]
    public class DeliveriersController : BaseApiController
    {
        private readonly IDeliverierService _deliverierService;

        public DeliveriersController(IDeliverierService deliverierService)
        {
            _deliverierService = deliverierService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliverierDto request)
        {
            var apiReponse = await _deliverierService.Create(request);

            return Response(apiReponse);
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> Update([FromBody] LicenseImageString request)
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
