using CoreGoDelivery.Domain.DTO.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier
{
    public class DeliverierServiceBase : BaseInternalServices
    {
        public static DeliverierEntity CreateToEntity(DeliverierDto data)
        {
            var result = new DeliverierEntity()
            {
                Id = IdBuild(data.Id),
                FullName = data.FullName,
                CNPJ = RemoveCharacteres(data.CNPJ),
                BirthDate = data.BirthDate,
                LicenceDriver = new LicenceDriverEntity()
                {
                    Id = data.LicenseNumber,
                    Type = ParseLicenseType(data),
                    ImageUrlReference = FileNameNormalize(data)
                }
            };

            return result;
        }

        public static string FileNameNormalize(DeliverierDto data)
        {
            var result = $"CNH_{data.LicenseNumber}.png";

            return result;
        }

        public static LicenseTypeEnum ParseLicenseType(DeliverierDto data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum licenseType);

            return licenseType;
        }
    }
}