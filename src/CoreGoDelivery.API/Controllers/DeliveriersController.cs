﻿using CoreGoDelivery.Api.Controllers.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreGoDelivery.Api.Controllers;

[Route("deliveriers")]
[ApiController]
public class DeliveriersController(IMediator _mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeliverierCreateCommand request)
    {
        var apiReponse = await _mediator.Send(request);

        return Response(apiReponse);
    }
}
