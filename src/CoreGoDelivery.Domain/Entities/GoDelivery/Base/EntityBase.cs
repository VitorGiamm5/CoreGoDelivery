namespace CoreGoDelivery.Domain.Entities.GoDelivery.Base;

public abstract class BaseEntity
{
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; set; }
}
