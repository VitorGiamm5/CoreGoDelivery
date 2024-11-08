namespace CoreGoDelivery.Domain.MinIOFile;

public sealed class MinIOSettings
{
    public string Endpoint { get; set; }
    public int Port { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public bool UseSSL { get; set; }
}
