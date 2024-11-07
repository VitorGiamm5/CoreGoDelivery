﻿using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
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

        if (command.IsUpdate)
        {
            var deliverier = await _repositoryDeliverier.GetOneById(command.IdDeliverier!);

            if (deliverier == null)
            {
                message.AppendError(message, nameof(command.IdDeliverier), AdditionalMessageEnum.NotFound);

                return _baseInternalServices.BuildMessageValidator(message);
            }
            else
            {
                command.Id = deliverier.LicenceDriverId;
            }
        }

        if (command.LicenseImageBase64.Length > FAULT_FILE_SIZE_LIMIT_MB * 1024 * 1024)
        {
            message.AppendError(message, FAULT_FILE_SIZE_MESSAGE, AdditionalMessageEnum.InvalidFormat);
        }

        if (!command.IsValidDeliverierUploadCnhCommand())
        {
            message.AppendError(message, command.StringFieldsName(), AdditionalMessageEnum.Required);
        }

        var (isValid, errorMessage, _) = _getExtensionFile.Build(command.LicenseImageBase64);

        if (!isValid)
        {
            message.AppendError(message, errorMessage, AdditionalMessageEnum.InvalidFormat);
        }

        return _baseInternalServices.BuildMessageValidator(message);
    }
}
