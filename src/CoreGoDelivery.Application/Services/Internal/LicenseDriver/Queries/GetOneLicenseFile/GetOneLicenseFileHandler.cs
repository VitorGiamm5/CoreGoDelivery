using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Domain.Enums.ServiceErrorMessage;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response.BaseResponse;
using CoreGoDelivery.Infrastructure.FileBucket.FileStorage;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Queries.GetOneLicenseFile;

public class GetOneLicenseFileHandler : IRequestHandler<GetOneLicenseFileCommand, ActionResult>
{
    public readonly IDeliverierRepository _repositoryDelivarier;
    public readonly ILicenceDriverRepository _repositoryLicence;
    public readonly IFileStorageService _fileService;

    public GetOneLicenseFileHandler(
        IDeliverierRepository repositoryDelivarier,
        ILicenceDriverRepository repositoryLicence,
        IFileStorageService fileService)
    {
        _repositoryDelivarier = repositoryDelivarier;
        _repositoryLicence = repositoryLicence;
        _fileService = fileService;
    }

    public async Task<ActionResult> Handle(GetOneLicenseFileCommand request, CancellationToken cancellationToken)
    {
        var apiResponse = new ActionResult();

        #region validation
        var deliverier = await _repositoryDelivarier.GetOneByIdLicense(request.Id);

        if (deliverier == null)
        {
            apiResponse.SetError("deliverier".AppendError(AdditionalMessageEnum.NotFound));
        }

        var license = await _repositoryLicence.GetOneById(request.Id);

        if (license == null)
        {
            apiResponse.SetError("license".AppendError(AdditionalMessageEnum.NotFound));
        }

        if (string.IsNullOrEmpty(license?.ImageUrlReference))
        {
            apiResponse.SetError("ImageUrlReference".AppendError(AdditionalMessageEnum.NotFound));
        }

        #endregion

        try
        {
            var base64File = await _fileService.GetFileAsync(license!.ImageUrlReference);

            if (base64File == null)
            {
                apiResponse.SetError("base64File".AppendError(AdditionalMessageEnum.NotFound));

                return apiResponse;
            }

            apiResponse.SetData(new { licenseImageBase64 = base64File });
        }
        catch (Exception ex)
        {
            apiResponse.SetError(ex.Message);
        }

        return apiResponse;
    }
}
