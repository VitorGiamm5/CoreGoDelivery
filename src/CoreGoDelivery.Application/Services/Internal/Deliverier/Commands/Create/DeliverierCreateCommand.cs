using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;
using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create
{
    public class DeliverierCreateCommand : DeliverierUploadCnhCommand, IRequest<ApiResponse>
    {
        [DefaultValue("entregador123")]
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [DefaultValue("João da Silva")]
        [JsonPropertyName("nome")]
        public string FullName { get; set; }

        [DefaultValue("12345678901234")]
        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [DefaultValue("1990-01-01T00:00:00Z")]
        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [DefaultValue("12345678900")]
        [JsonPropertyName("numero_cnh")]
        public string LicenseNumber { get; set; }

        [DefaultValue("A")]
        [JsonPropertyName("tipo_cnh")]
        public string LicenseType { get; set; }
    }
}
