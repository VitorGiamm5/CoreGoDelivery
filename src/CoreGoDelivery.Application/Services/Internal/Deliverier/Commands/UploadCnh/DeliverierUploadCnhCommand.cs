using CoreGoDelivery.Domain.DTO.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhCommand : IRequest<ApiResponse>
    {
        [DefaultValue("base64string")]
        [JsonPropertyName("imagem_cnh")]
        public string LicenseImageBase64 { get; set; }

        [JsonIgnore]
        public string? IdLicense { get; set; }
    }
}
