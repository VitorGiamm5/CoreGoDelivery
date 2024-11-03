using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;

public class DeliverierBuilderCreateImage
{
    public readonly ILicenceDriverRepository _repositoryLicense;

    public readonly DeliverierBuildFileName _buildFileName;
    public readonly DeliverierSaveOrReplaceLicenseImageAsync _saveLicenseFile;
    public readonly DeliverierBuildExtensionFile _buildExtensionFile;

    public DeliverierBuilderCreateImage(
        ILicenceDriverRepository repositoryLicense,
        DeliverierBuildFileName buildFileName,
        DeliverierSaveOrReplaceLicenseImageAsync saveLicenseFile,
        DeliverierBuildExtensionFile buildExtensionFile)
    {
        _repositoryLicense = repositoryLicense;
        _buildFileName = buildFileName;
        _saveLicenseFile = saveLicenseFile;
        _buildExtensionFile = buildExtensionFile;
    }

    public async Task<ApiResponse> Build(string UPLOAD_FOLDER, DeliverierUploadCnhCommand command, ApiResponse apiReponse)
    {
        var (_, _, fileExtension) = _buildExtensionFile.Build(command.LicenseImageBase64);

        var fileName = _buildFileName.Build(command.IdLicense!, fileExtension);

        await _repositoryLicense.UpdateFileName(command.IdLicense!, fileName);

        var imagePaths = await _saveLicenseFile.SaveOrReplace(command.LicenseImageBase64, fileName, UPLOAD_FOLDER);

        apiReponse.Data = new { message = $"Image of CNH was Created with success, path: {imagePaths}" };

        return apiReponse;
    }
}
