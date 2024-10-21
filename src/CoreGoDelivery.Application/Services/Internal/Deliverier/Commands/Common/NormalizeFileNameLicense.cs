using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common
{
    public class NormalizeFileNameLicense
    {
        public string Normalize(DeliverierCreateCommand data)
        {
            var result = $"CNH_{data.LicenseNumber}.png";

            return result;
        }
    }
}
