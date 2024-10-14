using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver
{
    public sealed class LicenceDriverEntity
    {
        [Key]
        public string LicenceDriverId { get; set; }

        public LicenceTypeEnum LicenceType { get; set; }

        public string ImageUrlReference { get; set; }
    }
}
