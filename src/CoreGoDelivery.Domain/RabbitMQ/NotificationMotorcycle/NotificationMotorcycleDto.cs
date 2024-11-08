namespace CoreGoDelivery.Application.Services.Internal.NotificationMotorcycle.Dto;

public sealed class NotificationMotorcycleDto
{
    public string Id { get; set; }
    public string ModelMotorcycleId { get; set; }
    public string PlateNormalized { get; set; }
    public string MotorcycleId { get; set; }
    public int YearManufacture { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
