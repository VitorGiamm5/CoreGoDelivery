using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.External.FileBucket;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.LicenseDriver.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver;

public class DeliverierUploadCnhHandler : IRequestHandler<LicenseImageCommand, ActionResult>
{
    public readonly ILicenceDriverRepository _repositoryLicense;

    public readonly LicenseDriverValidator _validator;
    public readonly CreateOrUpdateFileBucket _CreateOrUpdateImage;

    public readonly string BUCKET_NAME = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\uploads_cnh"));

    public DeliverierUploadCnhHandler(
        LicenseDriverValidator validator,
        CreateOrUpdateFileBucket builderUpdateImage,
        ILicenceDriverRepository repositoryLicense)
    {
        _validator = validator;
        _CreateOrUpdateImage = builderUpdateImage;
        _repositoryLicense = repositoryLicense;
    }

    public async Task<ActionResult> Handle(LicenseImageCommand command, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetMessage(await _validator.Build(command));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var license = await _repositoryLicense.GetOneById(command.IdLicenseNumber!);

        if (license == null)
        {
            apiReponse.SetMessage(nameof(license).AppendError(AdditionalMessageEnum.NotFound));

            return apiReponse;
        }

        if (license.IsPendingImage())
        {
            var (_, _, fileExtension) = ImageValidateExtensionFile.Build(command.LicenseImageBase64);

            license.ImageUrlReference = NameCreatorFile.LicenseDriver(command.IdLicenseNumber!, fileExtension);
        }

        var imagePath = NameCreatorFile.FileIntoBucket(BUCKET_NAME, license.ImageUrlReference);

        //var isSuccessFile = await _CreateOrUpdateImage.Build();
            // if(createdWithSuccess) await _repositoryLicense.UpdateFileName(command.IdLicenseNumber!, command.FileName);

        return apiReponse;
    }
}
