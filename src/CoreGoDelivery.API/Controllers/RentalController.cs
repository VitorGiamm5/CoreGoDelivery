using CoreGoDelivery.Api.Controllers.Base;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;
using CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("locacao")]
    [ApiController]
    public class RentalController(IMediator _mediator) : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string? id)
        {
            var result = await _mediator.Send(new RentalGetOneCommand(id));

            return Response(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentalCreateCommand request)
        {
            var result = await _mediator.Send(request);

            return Response(result);
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> UpdateReturnedToBaseDate(string id, [FromBody] RentalReturnedToBaseDateCommand request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);

            return Response(result);
        }
    }
}
