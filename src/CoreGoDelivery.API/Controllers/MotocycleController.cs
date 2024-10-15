using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("motos")]
    [ApiController]
    public class MotocycleController : BaseApiController
    {
        private readonly IMotocycleService _motocycleService;

        public MotocycleController(IMotocycleService motocycleService)
        {
            _motocycleService = motocycleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PlateIdDto? request)
        {
            var result = await _motocycleService.List(request);

            return Response(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _motocycleService.GetOne(id);

            return Response(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotocycleDto request)
        {
            var result = await _motocycleService.Create(request);

            return Response(result);
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] PlateIdDto request)
        {
            var result = await _motocycleService.ChangePlate(id, request);

            return Response(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(PlateIdDto id)
        {
            var result = await _motocycleService.Delete(id);

            return Response(result);
        }
    }
}
