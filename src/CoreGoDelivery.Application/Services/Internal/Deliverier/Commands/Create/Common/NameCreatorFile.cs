using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create.Common;

public static class NameCreatorFile
{
    public static string LicenseDriver(string licenseNumber, FileExtensionValidEnum fileExtension)
    {
        var fileName = new StringBuilder();

        fileName.Append($"CNH_{licenseNumber}");
        fileName.Append($".{fileExtension.ToString()}");

        return fileName.ToString();
    }

    public static string FileIntoBucket(string bucketName, string fileName)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append($"{bucketName}/");

        stringBuilder.Append(fileName);

        return stringBuilder.ToString();
    }
}
