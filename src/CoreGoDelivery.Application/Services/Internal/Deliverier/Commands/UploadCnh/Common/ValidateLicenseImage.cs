using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class ValidateLicenseImage
    {
        public readonly GetExtensionFile _getExtensionFile;

        public ValidateLicenseImage(GetExtensionFile getExtensionFile)
        {
            _getExtensionFile = getExtensionFile;
        }

        public (bool IsValid, string ErrorMessage, FileExtensionEnum FileExtension) Validate(byte[] base64Image)
        {
            //if (string.IsNullOrEmpty(base64Image))
            //{
            //    return (false, "A imagem da CNH não pode ser vazia.", FileExtensionEnum.none);
            //}

            try
            {
                //var imageBytes = Convert.FromBase64String(base64Image);

                if (base64Image.Length > 10 * 1024 * 1024)
                {
                    return (false, "A imagem não pode exceder 10 MB.", FileExtensionEnum.none);
                }

                var result = _getExtensionFile.Get(base64Image);

                return result;
            }
            catch (FormatException)
            {
                return (false, "A string fornecida não é uma imagem base64 válida.", FileExtensionEnum.none);
            }
        }
    }
}
