using CoreGoDelivery.Api.Controllers.Base;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.ChangePlateById;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Commands.Delete;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.GetOne;
using CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers;

[Route("api/motorcycle")]
[ApiController]
public class MotorcycleController(IMediator _mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] MotorcycleListQueryCommand request)
    {
        var result = await _mediator.Send(request);

        return Response(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        IdParamValidator(id);

        var result = await _mediator.Send(new MotorcycleGetOneQueryCommand(id));

        return Response(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MotorcycleCreateCommand request)
    {
        request.Id = IdBuild(request.Id);

        var result = await _mediator.Send(request);

        return Response(result);
    }

    [HttpPut("{id}/plate")]
    public async Task<IActionResult> Put(string id, [FromBody] MotorcycleChangePlateCommand request)
    {
        IdParamValidator(id);

        request.Id = id;

        var result = await _mediator.Send(request);

        return Response(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        IdParamValidator(id);

        var result = await _mediator.Send(new MotorcycleDeleteCommand(id));

        return Response(result);
    }
}
