using System.Text.Json.Serialization;

namespace CoreGoDelivery.Domain.Entities.GoDelivery.Base;

public abstract class BaseEntity
{
    [JsonIgnore]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public string CreatedBy { get; set; } = "admin";
}
