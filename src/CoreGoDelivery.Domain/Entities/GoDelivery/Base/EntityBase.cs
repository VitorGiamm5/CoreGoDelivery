namespace CoreGoDelivery.Domain.Entities.GoDelivery.Base;

public abstract class BaseEntity
{
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "admin";
}
