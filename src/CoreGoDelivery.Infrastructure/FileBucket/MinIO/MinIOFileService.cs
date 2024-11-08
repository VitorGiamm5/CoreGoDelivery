using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO;

public class MinIOFileService : IMinIOFileService
{

    // Initialize the client with access credentials.
    private static readonly IMinioClient _minioClient = new MinioClient()
                                        .WithEndpoint(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true" ? "minio" : "localhost", 9000)
                                        .WithCredentials("V5SAuF1U4Ma2zW9e8PgW", "OpsKYLv87DDjXNZMPYRaiB3Q4ivktSaZOmzVIJkK")
                                        .WithSSL(false)
                                        .Build();



    public async Task CreateBucketAsync(string bucketName)
    {
        var beArgs = new BucketExistsArgs().WithBucket(bucketName);

        bool found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(true);

        if (!found)
        {
            var mbArgs = new MakeBucketArgs().WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(true);
        }
    }

    public async Task<string> SaveOrReplace(string bucketName, string fileName, Stream fileStream, string contentType)
    {
        try
        {
            await CreateBucketAsync(bucketName);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(true);

            Console.WriteLine("Successfully uploaded " + fileName);

            return $"Successfully {fileName}";
        }
        catch (MinioException e)
        {
            Console.WriteLine("File Upload Error: {0}", e.Message);

            return $"File Upload Error: {{0}}\", e.Message";
        }
    }

    public async Task<string?> GetFileAsBase64Async(string bucketName, string fileName)
    {
        try
        {
            using var memoryStream = new MemoryStream();

            // Baixar o arquivo do bucket para o MemoryStream
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

            // Converter para Base64
            var base64 = Convert.ToBase64String(memoryStream.ToArray());

            return base64;
        }
        catch (ObjectNotFoundException)
        {
            return null;
        }
    }
}