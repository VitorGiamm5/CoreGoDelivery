using CoreGoDelivery.Domain.Response;
using MediatR;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhCommand : IRequest<ApiResponse>
    {
        [DefaultValue("base64string")]
        [JsonPropertyName("imagem_cnh")]
        public byte[] LicenseImageBase64 { get; set; }

        [JsonIgnore]
        public string? IdLicense { get; set; }

        [JsonIgnore]
        public string? IdDeliverier { get; set; }

        [JsonIgnore]
        public bool IsUpdate { get; set; }

        public bool HasIdDeliverier()
        {
            if (string.IsNullOrEmpty(IdDeliverier))
            {
                return false;
            }

            return true;
        }

        public bool HasIdLicense()
        {
            if (string.IsNullOrEmpty(IdLicense))
            {
                return false;
            }

            return true;
        }

        public bool IsValidDeliverierUploadCnhCommand()
        {
            if (LicenseImageBase64 == null)
            {
                return false;
            }

            if (!HasIdDeliverier() && !HasIdLicense())
            {
                return false;
            }

            return true;
        }

        public string StringFieldsName()
        {
            return $"{nameof(IdLicense)}: {IdLicense}, {nameof(IdDeliverier)}:{IdDeliverier}, {nameof(LicenseImageBase64)}:{LicenseImageBase64}";
        }
    }
}
