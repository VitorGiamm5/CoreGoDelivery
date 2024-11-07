using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;

public class DeliverierBuildMessageCnh
{
    public readonly ILicenceDriverRepository _repositoryLicence;

    public DeliverierBuildMessageCnh(ILicenceDriverRepository repositoryLicence)
    {
        _repositoryLicence = repositoryLicence;
    }

    public async Task Build(DeliverierCreateCommand data, StringBuilder message)
    {
        var licenseNumber = data.LicenseNumber;
        var paramName = nameof(licenseNumber);

        if (string.IsNullOrWhiteSpace(licenseNumber))
        {
            message.Append(paramName.AppendError());
        }
        else
        {
            var isValidLicense = CnhValidation.Validate(licenseNumber);

            if (!isValidLicense)
            {
                message.Append(paramName.AppendError());
            }

            var isUnicLicence = await _repositoryLicence.CheckIsUnicByLicence(licenseNumber);

            if (!isUnicLicence)
            {
                message.Append(paramName.AppendError(AdditionalMessageEnum.AlreadyExist));
            }
        }
    }
}
