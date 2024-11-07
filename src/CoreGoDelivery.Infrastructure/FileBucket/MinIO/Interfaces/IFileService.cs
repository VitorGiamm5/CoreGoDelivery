namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO.Interfaces;

public interface IFileService
{
    Task<string> SaveOrReplace(byte[] fileBytes, string fileNameWithExtension, string bucketName);
}