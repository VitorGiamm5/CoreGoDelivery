using Minio;

namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO;

public interface IMinIOClientService
{
    IMinioClient GetClient();
    Task EnsureBucketExistsAsync(string bucketName);
}
