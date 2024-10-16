using CoreGoDelivery.Application.Services.Internal.Interface;
using CoreGoDelivery.Domain.DTO.Rental;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("locacao")]
    [ApiController]
    public class RentalController : BaseApiController
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            var result = await _rentalService.GetById(id);

            return Response(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentalDto request)
        {
            var result = await _rentalService.Create(request);

            return Response(result);
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> UpdateReturnedToBaseDate(string id, [FromBody] ReturnedToBaseDateDto request)
        {
            var result = await _rentalService.UpdateReturnedToBaseDate(id, request);

            return Response(result);
        }
    }
}
