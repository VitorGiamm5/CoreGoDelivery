using CoreGoDelivery.Domain.Enums.LicenceDriverType;
using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver
{
    public sealed class LicenceDriverEntity
    {
        [Key]
        public string Id { get; set; }

        public LicenceTypeEnum Type { get; set; }

        public string FileNameImageNormalized { get; set; }
    }
}
