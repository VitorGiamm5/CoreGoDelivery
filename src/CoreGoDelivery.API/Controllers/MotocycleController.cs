using CoreGoDelivery.Domain.DTO.Motocycle;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreGoDelivery.Api.Controllers
{
    [Route("motos")]
    [ApiController]
    public class MotocycleController : ControllerBase
    {
        // GET: api/<MotocycleController>
        [HttpGet]
        [HttpGet]
        public async Task<List<MotocycleDto>> Get([FromQuery] PlateIdDto request)
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

            return result;
        }

        // GET api/<MotocycleController>/5
        [HttpGet("{id}")]
        public async Task<MotocycleDto> Get(string id)
        {
            var result = new MotocycleDto()
            {
                MotocycleId = "moto123",
                YearManufacture = 2020,
                ModelName = "Mottu Sport",
                PlateId = "CDX-0101"
            };

            return result;
        }

        // POST api/<MotocycleController>
        [HttpPost]
        public void Post([FromBody] MotocycleDto request)
        {
        }

        // PUT api/<MotocycleController>/5
        [HttpPut("{id}/placa")]
        public void Put(string id, [FromBody] PlateIdDto request)
        {
        }

        // DELETE api/<MotocycleController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
