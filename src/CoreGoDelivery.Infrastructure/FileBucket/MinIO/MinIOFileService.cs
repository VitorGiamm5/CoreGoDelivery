using Minio;
using Minio.DataModel.Args;

namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO;

public class MinIOFileService : IMinIOFileService
{
    private readonly MinioClient _minioClient;

    public MinIOFileService(MinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task CreateBucketAsync(string bucketName)
    {
        bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }
    }

    public async Task<string> SaveOrReplace(string bucketName, string fileName, Stream fileStream, string contentType)
    {
        // Garante que o bucket existe
        await CreateBucketAsync(bucketName);

        // Realiza o upload ou substitui o arquivo
        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType));

        return $"File '{fileName}' saved or replaced in bucket '{bucketName}'.";
    }
}
