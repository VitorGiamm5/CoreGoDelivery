using Microsoft.AspNetCore.Mvc;
using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("motos")]
    [ApiController]
    public class MotocycleController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PlateIdDto? request)
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

            return Response(apiReponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
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

            return Response(apiReponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotocycleDto request)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return Response(apiReponse);
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(string id, [FromBody] PlateIdDto request)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return Response(apiReponse);
        }

        /// <summary>
        /// Deleta uma empresa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(PlateIdDto id)
        {
            var apiReponse = new ApiResponse()
            {
                Message = null
            };

            return Response(apiReponse);
        }
    }
}
