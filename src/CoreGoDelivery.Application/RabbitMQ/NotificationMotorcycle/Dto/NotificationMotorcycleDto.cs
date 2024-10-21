namespace CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Dto
{
    public class NotificationMotorcycleDto
    {
        public string Id { get; set; }
        public string IdMotorcycle { get; set; }
        public int YearManufacture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
