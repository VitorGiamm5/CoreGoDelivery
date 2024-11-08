using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using CoreGoDelivery.Infrastructure.FileBucket.MinIO;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Queries.GetOneLicenseFile;

public class GetOneLicenseFileHandler : IRequestHandler<GetOneLicenseFileCommand, ActionResult>
{
    public readonly IDeliverierRepository _repositoryDelivarier;
    public readonly ILicenceDriverRepository _repositoryLicence;
    public readonly IMinIOFileService _fileService;

    public readonly string BUCKET_NAME = "license-cnh";

    public GetOneLicenseFileHandler(
        IDeliverierRepository repositoryDelivarier,
        ILicenceDriverRepository repositoryLicence,
        IMinIOFileService fileService)
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
            apiResponse.SetError("nao tem entregador");
        }

        var license = await _repositoryLicence.GetOneById(request.Id);

        if (license == null)
        {
            apiResponse.SetError("nao tem license");
        }

        if (string.IsNullOrEmpty(license?.ImageUrlReference))
        {
            apiResponse.SetError("nao tem ImageUrlReference");
        }

        #endregion

        try
        {
            var base64File = await _fileService.GetFileAsBase64Async(BUCKET_NAME, license!.ImageUrlReference);

            if (base64File == null)
            {
                apiResponse.SetError("nao achou o arquivo");

                return apiResponse;
            }

            apiResponse.SetData(new { licenseImageBase64 = base64File });
        }
        catch (FileNotFoundException ex)
        {
            apiResponse.SetError(ex.Message);
        }
        catch (Exception ex)
        {
            apiResponse.SetError(ex.Message);
        }

        return apiResponse;
    }
}
