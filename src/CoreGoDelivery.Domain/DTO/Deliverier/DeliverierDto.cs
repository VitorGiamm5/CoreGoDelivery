using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.DTO.Deliverier
{
    public class DeliverierDto
    {
        [JsonPropertyName("identificador")]
        public string DeliverierId { get; set; }

        [JsonPropertyName("nome")]
        public string FullName { get; set; }

        [JsonPropertyName("cnpj")]
        public string CNPJ { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("numero_cnh")]
        public string LicenseNumber { get; set; }

        [JsonPropertyName("tipo_cnh")]
        //[JsonConverter]
        public LicenceDriverTypeEnum LicenseType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string LicenseImageBase64 { get; set; }
    }
}
