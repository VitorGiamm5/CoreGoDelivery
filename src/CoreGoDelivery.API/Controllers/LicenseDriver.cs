using CoreGoDelivery.Api.Controllers.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers;

[Route("license-driver")]
[ApiController]
public class LicenseDriver(IMediator _mediator) : BaseApiController
{

    [HttpPost("{id}/upload-cnh")]
    public async Task<IActionResult> Upload(string id, [FromBody] LicenseImageCommand request)
    {
        IdParamValidator(id);

        request.Id = id;

        var apiReponse = await _mediator.Send(request);

        return Response(apiReponse);
    }
}
