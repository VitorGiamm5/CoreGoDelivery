using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using DocumentValidator;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators
{
    public class BuildMessageCnh
    {
        public readonly ILicenceDriverRepository _repositoryLicence;

        public BuildMessageCnh(ILicenceDriverRepository repositoryLicence)
        {
            _repositoryLicence = repositoryLicence;
        }

        public async Task Build(DeliverierCreateCommand data, StringBuilder message)
        {
            var licenseNumber = data.LicenseNumber;
            var paramName = nameof(licenseNumber);

            if (string.IsNullOrWhiteSpace(licenseNumber))
            {
                message.AppendError(message, paramName);
            }
            else
            {
                var isValidLicense = CnhValidation.Validate(licenseNumber);

                if (!isValidLicense)
                {
                    message.AppendError(message, paramName);
                }

                var isUnicLicence = await _repositoryLicence.CheckIsUnicByLicence(licenseNumber);

                if (!isUnicLicence)
                {
                    message.AppendError(message, paramName, AdditionalMessageEnum.AlreadyExist);
                }
            }
        }
    }
}
