using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.MessageValidators;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhHandler : IRequestHandler<DeliverierUploadCnhCommand, ApiResponse>
    {
        private readonly string _uploadFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\uploads_cnh"));

        public readonly NormalizeFileNameLicense _normalizeFileNameLicense;

        public DeliverierUploadCnhHandler(NormalizeFileNameLicense normalizeFileNameLicense)
        {
            _normalizeFileNameLicense = normalizeFileNameLicense;
        }

        public async Task<ApiResponse> Handle(DeliverierUploadCnhCommand command, CancellationToken cancellationToken)
        {
            var (isValid, errorMessage, fileExtension) = ValidateLicenseImage(command.LicenseImageBase64);
            
            if (!isValid)
            {
                return new ApiResponse
                {
                    Message = errorMessage
                };
            }

            var imagePath = await SaveOrReplaceLicenseImageAsync(command, fileExtension);

            return new ApiResponse { Message = "Imagem da CNH salva/substituída com sucesso", Data = imagePath };
        }

        private async Task<string> SaveOrReplaceLicenseImageAsync(DeliverierUploadCnhCommand command, FileExtensionEnum fileExtension)
        {
            var imageBytes = Convert.FromBase64String(command.LicenseImageBase64);

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder); // Cria a pasta caso ela não exista
            }

            var fileName = _normalizeFileNameLicense.Normalize(command.IdLicense!, fileExtension);
            
            var filePath = Path.Combine(_uploadFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            return filePath;
        }

        private (bool IsValid, string ErrorMessage, FileExtensionEnum FileExtension) ValidateLicenseImage(string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image))
                return (false, "A imagem da CNH não pode ser vazia.", FileExtensionEnum.none);

            try
            {
                var imageBytes = Convert.FromBase64String(base64Image);

                if (imageBytes.Length > 10 * 1024 * 1024)
                    return (false, "A imagem não pode exceder 10 MB.", FileExtensionEnum.none);

                using (var ms = new MemoryStream(imageBytes))
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                    {
                        return (true, "", FileExtensionEnum.png);
                    }
                    else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                    {
                        return (true, "", FileExtensionEnum.bpm);
                    }
                    else
                    {
                        return (false, "Somente imagens no formato PNG ou BMP são permitidas.", FileExtensionEnum.none);
                    }
                }
            }
            catch (FormatException)
            {
                return (false, "A string fornecida não é uma imagem base64 válida.", FileExtensionEnum.none);
            }
        }
    }
}
