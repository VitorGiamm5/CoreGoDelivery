using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Bmp;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common
{
    public class BuildExtensionFile
    {
        public (bool isValid, string errorMessage, FileExtensionValidEnum fileExtension) Build(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                try
                {
                    var image = Image.Load(ms); // Carrega a imagem sem o uso de `out`

                    if (image.Metadata.DecodedImageFormat is PngFormat)
                    {
                        return (true, "", FileExtensionValidEnum.png);
                    }
                    else if (image.Metadata.DecodedImageFormat is BmpFormat)
                    {
                        return (true, "", FileExtensionValidEnum.bmp);
                    }
                    else
                    {
                        return (false, "Format image file is invalid", FileExtensionValidEnum.none);
                    }
                }
                catch
                {
                    return (false, "Invalid image file", FileExtensionValidEnum.none);
                }
            }
        }
    }
}
