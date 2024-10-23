using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class BuildExtensionFile
    {
        public (bool isValid, string errorMessage, FileExtensionValidEnum fileExtension) Build(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                var image = System.Drawing.Image.FromStream(ms);

                if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                {
                    return (true, "", FileExtensionValidEnum.png);
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                {
                    return (true, "", FileExtensionValidEnum.bpm);
                }
                else
                {
                    return (false, "Format image file is invalid", FileExtensionValidEnum.none);
                }
            }
        }
    }
}
