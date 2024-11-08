using CoreGoDelivery.Api.Controllers.Base;
using CoreGoDelivery.Application.Services.Internal.LicenseDriver;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers;

[Route("api/license-driver")]
[ApiController]
public class LicenseDriver(IMediator _mediator) : BaseApiController
{

    [HttpPost("{id}/upload-cnh")]
    public async Task<IActionResult> Upload(string id, [FromForm] LicenseImageCommand request)
    {
        IdParamValidator(id);

        request.IdLicenseNumber = id;

        var apiReponse = await _mediator.Send(request);

        return Response(apiReponse);
    }
}
