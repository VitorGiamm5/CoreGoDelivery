using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierServiceBase
    {
        private const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public static string CnpjNormalize(DeliverierDto data)
        {
            var result = Regex.Replace(data.CNPJ, @"[./\s-]", "");

            return result;
        }

        public static string FileNameNormalize(DeliverierDto data)
        {
            var result = $"CNH_{data.LicenseNumber}.png";

            return result ;
        }

        public static string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }

        public static LicenceTypeEnum ParseLicenseType(DeliverierDto data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenceTypeEnum licenseType);

            return licenseType;
        }

        public static string SelectId(string id)
        {
            var result = string.IsNullOrEmpty(id) ? Ulid.NewUlid().ToString() : id;

            return result;
        }
    }
}