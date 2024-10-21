using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier
{
    public sealed class DeliverierEntity
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }

        #region relationships

        public string LicenceDriverId { get; set; }
        public LicenceDriverEntity LicenceDriver { get; set; }

        #endregion
    }
}
