using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Rental.Commands.Update;

public class RentalReturnedToBaseDateCommand : IRequest<ApiResponse>
{
    [DefaultValue("2024-01-07")]
    [JsonPropertyName("data_devolucao")]
    public DateTime? ReturnedToBaseDate { get; set; }

    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
}
