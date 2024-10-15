using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using CoreGoDelivery.Domain.Validators.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Deliverier
{
    public class DeliverierDto : LicenseImageString
    {
        [DefaultValue("entregador123")]
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [DefaultValue("João da Silva")]
        [JsonPropertyName("nome")]
        public string FullName { get; set; }

        [DefaultValue("12345678901234")]
        [JsonPropertyName("cnpj")]
        public string CNPJ { get; set; }

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
