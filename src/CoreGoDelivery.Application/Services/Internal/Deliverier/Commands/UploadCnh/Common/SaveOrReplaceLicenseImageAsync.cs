using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class SaveOrReplaceLicenseImageAsync
    {
        public readonly NormalizeFileNameLicense _normalizeFileNameLicense;

        public readonly string _uploadFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\uploads_cnh"));

        public SaveOrReplaceLicenseImageAsync(
            NormalizeFileNameLicense normalizeFileNameLicense)
        {
            _normalizeFileNameLicense = normalizeFileNameLicense;
        }

        public async Task<string> SaveOrReplace(DeliverierUploadCnhCommand command, GetFileExtensionValidEnum fileExtension)
        {
            //var imageBytes = Convert.FromBase64String(command.LicenseImageBase64);
            var imageBytes = command.LicenseImageBase64;

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
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
    }
}
