namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO;

public interface IMinIOFileService
{
    Task<string> SaveOrReplace(byte[] fileBytes, string fileNameWithExtension, string bucketName);
}