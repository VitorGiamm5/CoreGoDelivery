using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Domain.Response;
using MediatR;


namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhHandler : IRequestHandler<DeliverierUploadCnhCommand, ApiResponse>
    {
        public readonly IDeliverierRepository _repositoryDeliverier;
        public readonly IBaseInternalServices _baseInternalServices;

        public readonly NormalizeFileNameLicense _normalizeFileNameLicense;
        public readonly DeliverierUploadCnhValidator _deliverierUploadCnhValidator;
        public readonly ValidateLicenseImage _validateLicenseImage;
        public readonly SaveOrReplaceLicenseImageAsync _saveLicenseFile;
        public readonly GetExtensionFile _getExtensionFile;

        public DeliverierUploadCnhHandler(
            IDeliverierRepository repositoryDeliverier, 
            IBaseInternalServices baseInternalServices,
            NormalizeFileNameLicense normalizeFileNameLicense,
            DeliverierUploadCnhValidator deliverierUploadCnhValidator, 
            ValidateLicenseImage validateLicenseImage, 
            SaveOrReplaceLicenseImageAsync saveLicenseFile, 
            GetExtensionFile getExtensionFile)
        {
            _repositoryDeliverier = repositoryDeliverier;
            _baseInternalServices = baseInternalServices;
            _normalizeFileNameLicense = normalizeFileNameLicense;
            _deliverierUploadCnhValidator = deliverierUploadCnhValidator;
            _validateLicenseImage = validateLicenseImage;
            _saveLicenseFile = saveLicenseFile;
            _getExtensionFile = getExtensionFile;
        }

        public async Task<ApiResponse> Handle(DeliverierUploadCnhCommand command, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse
            {
                Data = null,
                Message = await _deliverierUploadCnhValidator.Build(command)
            };

            byte[] bitArrayImage = command.LicenseImageBase64;

            var (isValid, errorMessage, fileExtension) = _getExtensionFile.Get(command.LicenseImageBase64);

            var deliveier = await _repositoryDeliverier.GetOneById(command.IdDeliverier!);

            command.IdLicense = deliveier.LicenceDriverId;

            var imagePath = await _saveLicenseFile.SaveOrReplace(command, fileExtension);

            apiReponse.Data = $"Imagem da CNH salva/substituída com sucesso, path: {imagePath}";

            return apiReponse;
        }
    }
}
