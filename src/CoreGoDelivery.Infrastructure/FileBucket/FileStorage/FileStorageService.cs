using CoreGoDelivery.Domain.MinIOFile;
using CoreGoDelivery.Infrastructure.FileBucket.MinIO;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace CoreGoDelivery.Infrastructure.FileBucket.FileStorage;

public class FileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public FileStorageService(MinIOClientService minioClientService, IConfiguration configuration)
    {
        _minioClient = minioClientService.GetClient();
        _bucketName = configuration.GetValue<string>("MinIOSettings:BucketLicenseCnh")!;
    }

    public async Task UploadFileAsync(string fileName, Stream fileStream, string contentType)
    {
        try
        {
            bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
            }

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading file: {ex.Message}");
            throw;
        }
    }

    public async Task<FileResult> GetFileAsync(string fileName)
    {
        try
        {
            MemoryStream fileStream = new MemoryStream();

            // Baixa o arquivo do MinIO
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(fileStream);
                    fileStream.Position = 0;
                }));

            string base64Content = Convert.ToBase64String(fileStream.ToArray());

            return new FileResult
            {
                FileName = fileName,
                Base64Content = base64Content
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading file: {ex.Message}");
            throw;
        }
    }
}
