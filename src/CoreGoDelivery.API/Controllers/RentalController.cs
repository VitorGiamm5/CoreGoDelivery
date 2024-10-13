﻿using CoreGoDelivery.Domain.DTO.Rental;
using CoreGoDelivery.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("locacao")]
    [ApiController]
    public class RentalController : BaseApiController
    {
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = new RentalDto()
            {
                RentalId = "locacao123",
                DayliRate = 10,
                DeliveryPersonId = "valor_diaria",
                MotorcycleId = "moto123",
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now,
                EstimatedEndDate = DateTime.Now,
                ReturnedToBaseDate = DateTime.Now,
            };

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return Response(apiReponse);
        }

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReturnedToBaseDateDto request)
        {
            var apiReponse = new ApiResponse()
            {
                Data = null,
                Message = null
            };

            return Response(apiReponse);
        }

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> Put(string id, [FromBody] RentalDto request)
        {
            var result = new ResponseMessageDto()
            {
                Message = "Data de devolução informada com sucesso"
            };

            var apiReponse = new ApiResponse()
            {
                Data = result,
                Message = null
            };

            return Response(apiReponse);
        }
    }
}