namespace CoreGoDelivery.Domain.RabbitMQ;

public class RabbitMQSettings
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public QueuesNameSettings QueuesName { get; set; }

    public class QueuesNameSettings
    {
        public string MotorcycleQueue { get; set; }
    }
}