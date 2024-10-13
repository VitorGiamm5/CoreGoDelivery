using CoreGoDelivery.Domain.DTO.Motocycle;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("entregadores")]
    [ApiController]
    public class DeliveriersController : BaseApiController
    {
        // GET: api/<DeliveriersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeliveriersController>/5
        [HttpGet("{placa}")]
        public string Get(int placa)
        {
            return "value";
        }

        // POST api/<DeliveriersController>
        [HttpPost]
        public void Post([FromBody] MotocycleDto value)
        {
            
        }

        // PUT api/<DeliveriersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DeliveriersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
