namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO;

public interface IMinIOFileService
{
    Task CreateBucketAsync(string bucketName);
    Task<string> SaveOrReplace(string bucketName, string fileName, Stream fileStream, string contentType);
}