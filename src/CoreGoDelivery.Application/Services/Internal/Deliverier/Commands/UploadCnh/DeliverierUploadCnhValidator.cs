using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;

public class DeliverierUploadCnhValidator
{
    public readonly IBaseInternalServices _baseInternalServices;
    public readonly IDeliverierRepository _repositoryDeliverier;

    public readonly DeliverierBuildExtensionFile _getExtensionFile;

    public const string FAULT_FILE_SIZE_MESSAGE = "";
    public const int FAULT_FILE_SIZE_LIMIT_MB = 10;

    public DeliverierUploadCnhValidator(
        IBaseInternalServices baseInternalServices,
        IDeliverierRepository repositoryDeliverier,
        DeliverierBuildExtensionFile getExtensionFile)
    {
        _baseInternalServices = baseInternalServices;
        _repositoryDeliverier = repositoryDeliverier;
        _getExtensionFile = getExtensionFile;
    }

    public async Task<string?> Build(LicenseImageCommand command)
    {
        var message = new StringBuilder();

        var deliverier = await _repositoryDeliverier.GetOneById(command.IdDeliverier!);

        if (deliverier == null)
        {
            message.Append(nameof(command.IdDeliverier).AppendError(AdditionalMessageEnum.NotFound));

            return _baseInternalServices.BuildMessageValidator(message);
        }
        else
        {
            command.Id = deliverier.LicenceDriverId;
        }

        if (command.LicenseImageBase64.Length > FAULT_FILE_SIZE_LIMIT_MB * 1024 * 1024)
        {
            message.Append(nameof(command.LicenseImageBase64).AppendError(AdditionalMessageEnum.FileSizeInvalid));
        }

        if (!command.IsValidDeliverierUploadCnhCommand())
        {
            message.Append("LicenseImage".AppendError());
        }

        var (isValid, errorMessage, _) = _getExtensionFile.Build(command.LicenseImageBase64);

        if (!isValid)
        {
            message.Append("LicenseImage".AppendError(AdditionalMessageEnum.InvalidFormat));
        }

        return _baseInternalServices.BuildMessageValidator(message);
    }
}
