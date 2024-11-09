using CoreGoDelivery.Domain.Entities.GoDelivery.Base;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;

public sealed class DeliverierEntity : BaseEntity
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }

    #region relationships

    [JsonIgnore]
    public string LicenceDriverId { get; set; }

    [JsonIgnore]
    public LicenceDriverEntity LicenceDriver { get; set; }

    #endregion
}
