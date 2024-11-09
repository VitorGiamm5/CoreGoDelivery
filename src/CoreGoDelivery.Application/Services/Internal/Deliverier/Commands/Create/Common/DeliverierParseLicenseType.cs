using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.Common;

public static class DeliverierParseLicenseType
{
    public static LicenseTypeEnum Parse(DeliverierCreateCommand data)
    {
        Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum licenseType);

        return licenseType;
    }
}
