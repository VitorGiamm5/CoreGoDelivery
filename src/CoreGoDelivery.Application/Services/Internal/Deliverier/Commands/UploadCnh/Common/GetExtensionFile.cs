using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class GetFileExtensionValid
    {
        public (bool isValid, string errorMessage, GetFileExtensionValidEnum fileExtension) Get(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                var image = System.Drawing.Image.FromStream(ms);

                if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                {
                    return (true, "", GetFileExtensionValidEnum.png);
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                {
                    return (true, "", GetFileExtensionValidEnum.bpm);
                }
                else
                {
                    return (false, "Somente imagens no formato PNG ou BMP são permitidas.", GetFileExtensionValidEnum.none);
                }
            }
        }
    }
}
