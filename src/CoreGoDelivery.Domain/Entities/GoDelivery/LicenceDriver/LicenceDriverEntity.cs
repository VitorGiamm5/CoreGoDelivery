using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver
{
    public sealed class LicenceDriverEntity
    {
        public string Id { get; set; }
        public LicenseTypeEnum Type { get; set; }
        public string ImageUrlReference { get; set; }
    }
}
