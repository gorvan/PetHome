using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetHome.Application.FileProvider;
using PetHome.Application.Pets.Files.Delete;
using PetHome.Application.Pets.Files.GetFile;
using PetHome.Application.Pets.Files.GetFiles;
using PetHome.Application.Providers;
using PetHome.Domain.Shared;
using System.Reactive.Linq;

namespace PetHome.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private int DEFAULT_EXPIRY = 604800;
        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;


        public MinioProvider(IMinioClient minioClient,
            ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<string>> UploadFile(FileData fileData, CancellationToken token)
        {
            try
            {
                var checkBucketsArgs = new BucketExistsArgs()
               .WithBucket(fileData.BucketName);

                var checkResult =
                    await _minioClient.BucketExistsAsync(checkBucketsArgs, token);

                if (checkResult == false)
                {
                    var newBacket = new MakeBucketArgs().WithBucket(fileData.BucketName);
                    await _minioClient.MakeBucketAsync(newBacket, token);
                }

                ////var path = Guid.NewGuid();

                var args = new PutObjectArgs()
                    .WithBucket(fileData.BucketName)
                    .WithStreamData(fileData.Stream)
                    .WithObjectSize(fileData.Stream.Length)
                    .WithObject(fileData.ObjectName);

                var result = await _minioClient.PutObjectAsync(args, token);
                return result.ObjectName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio");
            }
        }

        public async Task<Result<string>> GetFile(GetFileCommand fileData)
        {
            try
            {
                var bucketArgs = new PresignedGetObjectArgs()
                    .WithBucket(fileData.BucketName)
                    .WithObject(fileData.FilePath)
                    .WithExpiry(DEFAULT_EXPIRY);

                var getResult = await _minioClient.PresignedGetObjectAsync(bucketArgs)
                    .ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(getResult))
                {
                    Error.Failure("file.get", "Fail to get file from minio");
                }

                return getResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to get file from minio");
                return Error.Failure("file.get", "Fail to get file from minio");
            }
        }

        public async Task<Result<List<string>>> GetFiles(
            GetFilesCommand fileData,
            CancellationToken token)
        {
            try
            {
                var bucketArgs = new ListObjectsArgs()
                .WithBucket(fileData.BucketName);

                var getResult = await _minioClient.ListObjectsAsync(bucketArgs, token).ToList();


                if (getResult == null)
                {
                    return Error.Failure("files.get", "Fail to get files from minio");
                }

                return getResult.Select(b => b.Key).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to get files from minio");
                return Error.Failure("files.get", "Fail to get files from minio");
            }
        }

        public async Task<Result<bool>> DeleteFile(
            DeleteFileCommand fileData,
            CancellationToken token)
        {
            try
            {
                var removeArgs = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FilePath);

                await _minioClient.RemoveObjectAsync(removeArgs, token)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to delete file in minio");
                return Error.Failure("file.delete", "Fail to delete file in minio");
            }
        }        
    }
}
