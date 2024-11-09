﻿using CoreGoDelivery.Domain.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Queries.GetOne;

public class RentalGetOneCommand : IRequest<ActionResult>
{
    public RentalGetOneCommand(string id)
    {
        Id = id;
    }

    [JsonIgnore]
    public string Id { get; set; }
}
