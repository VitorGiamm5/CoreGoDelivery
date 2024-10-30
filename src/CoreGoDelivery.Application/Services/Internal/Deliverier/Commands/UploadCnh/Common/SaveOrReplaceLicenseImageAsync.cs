using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class SaveOrReplaceLicenseImageAsync
    {
        public readonly BuildFileName _normalizeFileNameLicense;



        public SaveOrReplaceLicenseImageAsync(
            BuildFileName normalizeFileNameLicense)
        {
            _normalizeFileNameLicense = normalizeFileNameLicense;
        }

        public async Task<string> SaveOrReplace(byte[] licenseImageBase64, string fileName, string uploadFolder)
        {
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var filePath = Path.Combine(uploadFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await System.IO.File.WriteAllBytesAsync(filePath, licenseImageBase64);

            return filePath;
        }
    }
}
