using CoreGoDelivery.Api.Controllers.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers
{
    [Route("entregadores")]
    [ApiController]
    public class DeliveriersController(IMediator _mediator) : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliverierCreateCommand request)
        {
            var apiReponse = await _mediator.Send(request);

            return Response(apiReponse);
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> Upload(string id, [FromBody] DeliverierUploadCnhCommand request)
        {
            request.IdLicense = id;

            var apiReponse = await _mediator.Send(id);

            return Response(apiReponse);
        }
    }
}
