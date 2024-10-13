using CoreGoDelivery.Domain.Enums.LicenceDriverType;
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
