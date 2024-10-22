using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.Text;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common
{
    public class NormalizeFileNameLicense
    {
        public string Normalize(string licenseNumber, FileExtensionEnum fileExtension)
        {
            var fileName = new StringBuilder();

            fileName.Append($"CNH_{licenseNumber}");
            fileName.Append($".{fileExtension.ToString()}");

            var result = fileName.ToString();

            return result;
        }
    }
}
