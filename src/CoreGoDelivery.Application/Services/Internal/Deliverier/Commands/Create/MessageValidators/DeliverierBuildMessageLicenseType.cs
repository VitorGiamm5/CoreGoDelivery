using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;

public class DeliverierBuildMessageLicenseType
{
    public void Build(DeliverierCreateCommand data, StringBuilder message)
    {
        var paramName = nameof(data.LicenseType);

        if (!Enum.TryParse(data.LicenseType, ignoreCase: true, out LicenseTypeEnum _))
        {
            message.AppendError(message, paramName);
        }
    }
}
