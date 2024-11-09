using CoreGoDelivery.Domain.Consts;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Png;

namespace CoreGoDelivery.Application.Services.Internal.LicenseDriver.Commands.Create.Common;

public static class ImageValidateExtensionFile
{
    public static (bool isValid, string errorMessage, FileExtensionValidEnum fileExtension) Build(byte[] imageBytes)
    {
        using var ms = new MemoryStream(imageBytes);

        try
        {
            var image = Image.Load(ms);

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
                return (false, DeliverierServiceConst.MESSAGE_FORMAT_FILE_INVALID, FileExtensionValidEnum.none);
            }
        }
        catch
        {
            return (false, DeliverierServiceConst.MESSAGE_FORMAT_FILE_DEFAULT, FileExtensionValidEnum.none);
        }
    }
}
