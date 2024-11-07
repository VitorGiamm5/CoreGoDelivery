using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;

public class DeliverierBuilderCreateImage
{
    public readonly ILicenceDriverRepository _repositoryLicense;

    public DeliverierBuilderCreateImage(ILicenceDriverRepository repositoryLicense)
    {
        _repositoryLicense = repositoryLicense;
    }

    public async Task<ActionResult> Build(string UPLOAD_FOLDER, LicenseImageCommand command, ActionResult apiReponse)
    {
        var (_, _, fileExtension) = DeliverierBuildExtensionFile.Build(command.LicenseImageBase64);

        var fileName = DeliverierBuildFileName.Build(command.Id!, fileExtension);

        await _repositoryLicense.UpdateFileName(command.Id!, fileName);

        var imagePaths = await DeliverierSaveOrReplaceLicenseImageAsync.SaveOrReplace(command.LicenseImageBase64, fileName, UPLOAD_FOLDER);

        apiReponse.Data = new { message = $"Image of CNH was Created with success, path: {imagePaths}" };

        return apiReponse;
    }
}
