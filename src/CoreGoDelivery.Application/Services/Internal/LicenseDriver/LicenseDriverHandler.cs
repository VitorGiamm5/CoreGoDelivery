using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.LicenseDriver.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using CoreGoDelivery.Infrastructure.FileBucket.MinIO;
using CoreGoDelivery.Infrastructure.FileBucket.MinIO.Extensions;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver;

public class LicenseDriverHandler : IRequestHandler<LicenseImageCommand, ActionResult>
{
    public readonly ILicenceDriverRepository _repositoryLicense;
    private readonly IMinIOFileService _fileService;

    public readonly LicenseDriverValidator _validator;

    public readonly string BUCKET_NAME = "licensecnh";

    public LicenseDriverHandler(ILicenceDriverRepository repositoryLicense, IMinIOFileService fileService, LicenseDriverValidator validator)
    {
        _repositoryLicense = repositoryLicense;
        _fileService = fileService;
        _validator = validator;
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

        using Stream stream = new MemoryStream(command.LicenseImageBase64);

        var x = await _fileService.SaveOrReplace(BUCKET_NAME, license.ImageUrlReference, stream, GetContentType.Get(license.ImageUrlReference));


        //TODO: HEre File



        //if (isSuccessFile != null)
        //{
        //    await _repositoryLicense.UpdateFileName(command.IdLicenseNumber!, license.ImageUrlReference);
        //}

        return apiReponse;
    }
}
