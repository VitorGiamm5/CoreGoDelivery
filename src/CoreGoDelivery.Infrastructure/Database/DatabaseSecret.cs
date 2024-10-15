using System.Text;

namespace CoreGoDelivery.Infrastructure.Database
{
    public sealed class DatabaseSecret
    {
        public string Host { get; set; }
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
