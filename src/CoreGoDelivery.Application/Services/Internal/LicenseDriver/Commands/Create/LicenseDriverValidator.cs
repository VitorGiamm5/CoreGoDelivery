using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create;

public class LicenseDriverValidator
{
    public readonly ILicenceDriverRepository _repositoryLicense;

    public readonly IDeliverierRepository _repositoryDeliverier;

    public const string FAULT_FILE_SIZE_MESSAGE = "";
    public const int FAULT_FILE_SIZE_LIMIT_MB = 10;

    public LicenseDriverValidator(ILicenceDriverRepository repositoryLicense, IDeliverierRepository repositoryDeliverier)
    {
        _repositoryLicense = repositoryLicense;
        _repositoryDeliverier = repositoryDeliverier;
    }

    public async Task<StringBuilder> Build(LicenseImageCommand command)
    {
        var message = new StringBuilder();

        #region Verify if deliverier exists

        var deliverier = await _repositoryDeliverier.GetOneByIdLicense(command.IdLicenseNumber!);

        if (deliverier == null)
        {
            message.Append(nameof(deliverier).AppendError(AdditionalMessageEnum.NotFound));

            return message;
        }

        #endregion

        #region Verify if the license driver exist

        var licenseDriver = _repositoryLicense.GetOneById(command.IdLicenseNumber!);

        if (licenseDriver == null)
        {
            message.Append(nameof(licenseDriver).AppendError(AdditionalMessageEnum.NotFound));

            return message;
        }

        #endregion

        #region Verity file size

        if (command.LicenseImageBase64.LongLength < 10)
        {
            message.Append(nameof(command.LicenseImageBase64).AppendError());
        }

        if (command.LicenseImageBase64.Length > FAULT_FILE_SIZE_LIMIT_MB * 1024 * 1024)
        {
            message.Append(nameof(command.LicenseImageBase64).AppendError(AdditionalMessageEnum.FileSizeInvalid));
        }

        #endregion

        #region verity file extension

        var (isValid, errorMessage, _) = ImageValidateExtensionFile.Build(command.LicenseImageBase64);

        if (!isValid)
        {
            message.Append($"LicenseImage: {errorMessage}".AppendError(AdditionalMessageEnum.InvalidFormat));
        }

        #endregion

        return message;
    }
}
