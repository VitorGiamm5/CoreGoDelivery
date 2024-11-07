using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;

public class DeliverierBuilderUpdateImage
{
    public readonly ILicenceDriverRepository _repositoryLicense;
    public readonly IDeliverierRepository _repositoryDeliverier;

    public DeliverierBuilderUpdateImage(
        ILicenceDriverRepository repositoryLicense,
        IDeliverierRepository repositoryDeliverier)
    {
        _repositoryLicense = repositoryLicense;
        _repositoryDeliverier = repositoryDeliverier;
    }

    public async Task<ActionResult> Build(string UPLOAD_FOLDER, LicenseImageCommand command, ActionResult apiReponse)
    {
        var deliverier = await _repositoryDeliverier.GetOneById(command.IdDeliverier!);

        if (deliverier == null)
        {
            apiReponse.SetMessage(nameof(command.IdDeliverier).AppendError(AdditionalMessageEnum.NotFound));

            return apiReponse;
        }

        command.Id = deliverier.LicenceDriverId;

        var (_, _, fileExtension) = DeliverierBuildExtensionFile.Build(command.LicenseImageBase64);

        var fileName = DeliverierBuildFileName.Build(command.Id!, fileExtension);

        await _repositoryLicense.UpdateFileName(deliverier.LicenceDriverId, fileName);

        var imagePaths = await DeliverierSaveOrReplaceLicenseImageAsync.SaveOrReplace(command.LicenseImageBase64, fileName, UPLOAD_FOLDER);

        apiReponse.Data = new
        {
            message = $"${DeliverierServiceConst.MESSAGE_CNH_UPDATED_SUCCESS}, path: {imagePaths}"
        };

        return apiReponse;
    }
}
