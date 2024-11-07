using CoreGoDelivery.Infrastructure.FileBucket.MinIO.Extensions;
using CoreGoDelivery.Infrastructure.FileBucket.MinIO.Interfaces;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO;

public class MinIOFileService : IFileService
{
    private readonly MinioClient _minioClient;

    public MinIOFileService(MinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> SaveOrReplace(byte[] fileBytes, string fileNameWithExtension, string bucketName)
    {
        using var stream = new MemoryStream(fileBytes);

        // Verifica se o bucket existe; se não, cria o bucket
        bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }

        // Remove o arquivo existente se já estiver no bucket
        try
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(bucketName).WithObject(fileNameWithExtension));
        }
        catch (MinioException ex) when (ex is ObjectNotFoundException)
        {
            // Arquivo não existe, então não é necessário remover
        }

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileNameWithExtension)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(GetContentType.Get(fileNameWithExtension)));

        return fileNameWithExtension;
    }
}
