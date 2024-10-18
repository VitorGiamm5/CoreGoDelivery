﻿using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Motorcycle
{
    public class PlateIdDto
    {
        [DefaultValue("SDFZXCV")]
        [JsonPropertyName("placa")]
        public string? Placa { get; set; }
    };
}