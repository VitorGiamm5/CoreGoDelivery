using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Motorcycle;
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
        public async Task<IActionResult> List([FromQuery] PlateIdDto? request)
        {
            var result = await _motocycleService.List(request?.Placa);

            return Response(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            var result = await _motocycleService.GetOne(id);

            return Response(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotorcycleDto request)
        {
            var result = await _motocycleService.Create(request);

            return Response(result);
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] PlateIdDto request)
        {
            string? plate = request.Placa;

            var result = await _motocycleService.ChangePlateById(id, plate);

            return Response(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _motocycleService.DeleteById(id);

            return Response(result);
        }
    }
}
