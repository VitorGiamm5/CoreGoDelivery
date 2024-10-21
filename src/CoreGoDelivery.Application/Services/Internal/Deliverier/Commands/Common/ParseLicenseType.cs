using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common
{
    public class ParseLicenseType
    {
        public LicenseTypeEnum Parse(DeliverierCreateCommand data)
        {
            Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum licenseType);

            return licenseType;
        }
    }
}
