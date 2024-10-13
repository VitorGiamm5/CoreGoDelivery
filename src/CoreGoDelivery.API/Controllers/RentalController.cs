﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreGoDelivery.Api.Controllers
{
    [Route("locacao")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        // GET: api/<RentalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RentalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RentalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RentalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RentalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
