using Azure.Core;
using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.Common;
using CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create.Common;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response.BaseResponse;
using CoreGoDelivery.Infrastructure.FileBucket.ContentType;
using CoreGoDelivery.Infrastructure.FileBucket.FileStorage;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create;

public class LicenseDriverHandler : IRequestHandler<LicenseImageCommand, ActionResult>
{
    public readonly ILicenceDriverRepository _repositoryLicense;
    public readonly IFileStorageService _fileService;
    public readonly LicenseDriverValidator _validator;

    public LicenseDriverHandler(
        ILicenceDriverRepository repositoryLicense,
        IFileStorageService fileService,
        LicenseDriverValidator validator)
    {
        _repositoryLicense = repositoryLicense;
        _fileService = fileService;
        _validator = validator;
    }

    public async Task<ActionResult> Handle(LicenseImageCommand command, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetError(await _validator.Build(command));

        if (apiReponse.HasError())
        {
            return apiReponse;
        }

        var license = await _repositoryLicense.GetOneById(command.IdLicenseNumber!);

        if (license == null)
        {
            apiReponse.SetError(nameof(license).AppendError(AdditionalMessageEnum.NotFound));

            return apiReponse;
        }

        var (_, _, fileExtension) = ImageValidateExtensionFile.Build(command.LicenseImageBase64);

        license.ImageUrlReference = NameCreatorFile.LicenseDriver(command.IdLicenseNumber!, fileExtension);

        await _repositoryLicense.UpdateFileName(license.Id, license.ImageUrlReference);
    
        using Stream stream = new MemoryStream(command.LicenseImageBase64);

        var contentType = GetContentType.Get(license.ImageUrlReference);

        try
        {
            await _fileService.UploadFileAsync(license.ImageUrlReference, stream, contentType);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            apiReponse.SetError(ex.Message);
        }

        apiReponse.SetData(new 
        { 
            message = $"License image accepted", 
            metadata = new
            {
                license.Id,
                license.ImageUrlReference,
                contentType
            }
        });

        return apiReponse;
    }
}
