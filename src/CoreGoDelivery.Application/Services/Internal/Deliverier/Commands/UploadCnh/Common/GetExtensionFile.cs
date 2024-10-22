using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class GetExtensionFile
    {
        public (bool IsValid, string ErrorMessage, FileExtensionEnum FileExtension) Get(byte[] imageBytes)
        {
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
    }
}
