using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier
{
    public sealed class DeliverierEntity
    {
        [Key]
        public string Id { get; set; }

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
