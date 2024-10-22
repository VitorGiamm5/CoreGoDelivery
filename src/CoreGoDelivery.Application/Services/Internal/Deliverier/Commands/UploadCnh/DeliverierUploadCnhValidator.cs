using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhValidator
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IDeliverierRepository _repositoryDeliverier;

        public readonly ValidateLicenseImage _validateLicenseImage;

        public DeliverierUploadCnhValidator(
            IBaseInternalServices baseInternalServices, 
            IDeliverierRepository repositoryDeliverier, 
            ValidateLicenseImage validateLicenseImage)
        {
            _baseInternalServices = baseInternalServices;
            _repositoryDeliverier = repositoryDeliverier;
            _validateLicenseImage = validateLicenseImage;
        }

        public async Task<string?> Build(DeliverierUploadCnhCommand command)
        {
            var message = new StringBuilder();

            var (isValid, errorMessage, FileExtension) = _validateLicenseImage.Validate(command.LicenseImageBase64);

            if (!isValid)
            {
                message.AppendError(message, errorMessage, AdditionalMessageEnum.InvalidFormat);
            }

            if (!command.IsValidDeliverierUploadCnhCommand())
            {
                message.AppendError(message, command.StringFieldsName(), AdditionalMessageEnum.Required);
            }

            if (!command.HasIdDeliverier())
            {
                if (command.HasIdDeliverier())
                {
                    var deliverier = await _repositoryDeliverier.GetOneById(command.IdDeliverier!);

                    if (deliverier == null)
                    {
                        message.AppendError(message, CommomMessagesConst.MESSAGE_INVALID_DATA);
                    }
                }
                else
                {
                    message.AppendError(message, CommomMessagesConst.MESSAGE_INVALID_DATA);
                }
            }

            return _baseInternalServices.BuildMessageValidator(message);
        }
    }
}
