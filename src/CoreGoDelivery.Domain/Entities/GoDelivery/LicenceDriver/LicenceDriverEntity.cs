using CoreGoDelivery.Domain.Entities.GoDelivery.Base;
using CoreGoDelivery.Domain.Enums.LicenceDriverType;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

public sealed class LicenceDriverEntity : BaseEntity
{
    public string Id { get; set; }
    public LicenseTypeEnum Type { get; set; }
    public string ImageUrlReference { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string DeliverierId { get; set; }
}
