using CoreGoDelivery.Domain.MinIOFile;

namespace CoreGoDelivery.Infrastructure.FileBucket.FileStorage;

public interface IFileStorageService
{
    Task UploadFileAsync(string fileName, Stream fileStream, string contentType);
    Task<FileResult> GetFileAsync(string fileName);
}