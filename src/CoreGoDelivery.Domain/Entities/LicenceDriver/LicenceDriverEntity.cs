using CoreGoDelivery.Domain.Enums.Delivery;
using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.LicenceDriver
{
    public class LicenceDriverEntity
    {
        [Key]
        public string LicenceDriverId { get; set; }

        public LicenceDriverTypeEnum LicenceDriverType { get; set; }

        public string ImageUrlReference { get; set; }
    }
}
