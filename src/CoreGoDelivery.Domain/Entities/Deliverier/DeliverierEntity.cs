using CoreGoDelivery.Domain.Entities.LicenceDriver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.Entities.Deliverier
{
    public class DeliverierEntity
    {
        [Key]
        public string DeliverierId { get; set; }

        public string FullName { get; set; }

        public string CNPJ { get; set; }

        public DateTime BirthDate { get; set; }

        #region relationships

        [ForeignKey(nameof(LicenceDriver))]
        public string LicenseNumberId { get; set; }
        public LicenceDriverEntity LicenceDriver { get; set; }

        #endregion
    }
}
