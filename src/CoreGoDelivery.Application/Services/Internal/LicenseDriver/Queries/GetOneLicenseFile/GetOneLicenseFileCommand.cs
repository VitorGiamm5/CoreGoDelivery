﻿using CoreGoDelivery.Domain.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Queries.GetOneLicenseFile;

public class GetOneLicenseFileCommand : IRequest<ActionResult>
{
    public GetOneLicenseFileCommand(string id)
    {
        Id = id;
    }

    [JsonIgnore]
    public string Id { get; set; }
}
