using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhValidator
    {
        public readonly IBaseInternalServices _baseInternalServices;
        public readonly IDeliverierRepository _repositoryDeliverier;

        public readonly GetFileExtensionValid _getExtensionFile;

        public DeliverierUploadCnhValidator(
            IBaseInternalServices baseInternalServices,
            IDeliverierRepository repositoryDeliverier,
            GetFileExtensionValid getExtensionFile)
        {
            _baseInternalServices = baseInternalServices;
            _repositoryDeliverier = repositoryDeliverier;
            _getExtensionFile = getExtensionFile;
        }

        public async Task<string?> Build(DeliverierUploadCnhCommand command)
        {
            var message = new StringBuilder();

            if (command.LicenseImageBase64.Length > 10 * 1024 * 1024)
            {
                message.AppendError(message, "A imagem não pode exceder 10 MB.", AdditionalMessageEnum.InvalidFormat);
            }

            if (!command.IsValidDeliverierUploadCnhCommand())
            {
                message.AppendError(message, command.StringFieldsName(), AdditionalMessageEnum.Required);
            }

            if (command.HasIdDeliverier())
            {
                var deliverier = command?.IdDeliverier != null
                    ? await _repositoryDeliverier.GetOneById(command.IdDeliverier)
                    : null;

                if (deliverier == null)
                {
                    message.AppendError(message, nameof(command.IdDeliverier), AdditionalMessageEnum.InvalidFormat);
                }
            }

            if (command!.HasIdLicense())
            {
                var deliverier = await _repositoryDeliverier.GetOneByIdLicense(command.IdLicense!);

                if (deliverier == null)
                {
                    message.AppendError(message, nameof(command.IdLicense), AdditionalMessageEnum.InvalidFormat);
                }
            }

            var (isValid, errorMessage, fileExtension) = _getExtensionFile.Get(command.LicenseImageBase64);

            if (!isValid)
            {
                message.AppendError(message, errorMessage, AdditionalMessageEnum.InvalidFormat);
            }

            return _baseInternalServices.BuildMessageValidator(message);
        }
    }
}
