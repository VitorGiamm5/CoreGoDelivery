using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;

public class DeliverierBuilderUpdateImage
{
    public readonly ILicenceDriverRepository _repositoryLicense;
    public readonly IDeliverierRepository _repositoryDeliverier;

    public readonly DeliverierBuildFileName _buildFileName;
    public readonly DeliverierSaveOrReplaceLicenseImageAsync _saveLicenseFile;
    public readonly DeliverierBuildExtensionFile _buildExtensionFile;

    public DeliverierBuilderUpdateImage(
        ILicenceDriverRepository repositoryLicense,
        IDeliverierRepository repositoryDeliverier,
        DeliverierBuildFileName buildFileName,
        DeliverierSaveOrReplaceLicenseImageAsync saveLicenseFile,
        DeliverierBuildExtensionFile buildExtensionFile)
    {
        _repositoryLicense = repositoryLicense;
        _repositoryDeliverier = repositoryDeliverier;
        _buildFileName = buildFileName;
        _saveLicenseFile = saveLicenseFile;
        _buildExtensionFile = buildExtensionFile;
    }

    public async Task<ApiResponse> Build(string UPLOAD_FOLDER, DeliverierUploadCnhCommand command, ApiResponse apiReponse)
    {
        var deliverier = await _repositoryDeliverier.GetOneById(command.IdDeliverier!);

        if (deliverier == null)
        {
            apiReponse.Message = "Error";

            return apiReponse;
        }

        command.IdLicense = deliverier.LicenceDriverId;

        var (_, _, fileExtension) = _buildExtensionFile.Build(command.LicenseImageBase64);

        var fileName = _buildFileName.Build(command.IdLicense!, fileExtension);

        await _repositoryLicense.UpdateFileName(deliverier.LicenceDriverId, fileName);

        var imagePaths = await _saveLicenseFile.SaveOrReplace(command.LicenseImageBase64, fileName, UPLOAD_FOLDER);

        apiReponse.Data = new { message = $"${DeliverierServiceConst.MESSAGE_CNH_UPDATED_SUCCESS}, path: {imagePaths}" };

        return apiReponse;
    }
}
