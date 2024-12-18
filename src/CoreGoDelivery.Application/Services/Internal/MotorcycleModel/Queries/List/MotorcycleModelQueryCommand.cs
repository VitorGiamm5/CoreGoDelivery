﻿using CoreGoDelivery.Domain.Response.BaseResponse;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.MotorcycleModel.Queries.List;

public class MotorcycleModelQueryCommand : IRequest<ActionResult>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}
