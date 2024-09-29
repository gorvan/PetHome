using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetHome.Application.FileProvider;
using PetHome.Domain.Shared;
using System.Reactive.Linq;
using FileInfo = PetHome.Application.FileProvider.FileInfo;

namespace PetHome.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private readonly int DEFAULT_EXPIRY = 604800;
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
                    .WithBucket(fileData.Info.BucketName);

                var checkResult =
                    await _minioClient.BucketExistsAsync(checkBucketsArgs, token);

                if (checkResult == false)
                {
                    var newBucket = new MakeBucketArgs().WithBucket(fileData.Info.BucketName);
                    await _minioClient.MakeBucketAsync(newBucket, token);
                }

                var args = new PutObjectArgs()
                    .WithBucket(fileData.Info.BucketName)
                    .WithStreamData(fileData.Stream)
                    .WithObjectSize(fileData.Stream.Length)
                    .WithObject(fileData.Info.FilePath);

                var result = await _minioClient.PutObjectAsync(args, token);
                return result.ObjectName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio");
            }
        }

        public async Task<Result<string>> GetFile(FileInfo fileInfo)
        {
            try
            {
                var bucketArgs = new PresignedGetObjectArgs()
                    .WithBucket(fileInfo.BucketName)
                    .WithObject(fileInfo.FilePath)
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
            FileInfo fileInfo,
            CancellationToken token)
        {
            try
            {
                var bucketArgs = new ListObjectsArgs()
                .WithBucket(fileInfo.BucketName);

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

        public async Task<Result> RemoveFile(FileInfo fileInfo, CancellationToken token)
        {
            try
            {
                await IfBacketsNotExistCreateBucket(fileInfo.BucketName, token);

                var statArgs = new StatObjectArgs()
                    .WithBucket(fileInfo.BucketName)
                    .WithObject(fileInfo.FilePath);

                var objectStat = await _minioClient.StatObjectAsync(statArgs, token);
                if (objectStat is null)
                {
                    return Result.Success();
                }

                var removeArgs = new RemoveObjectArgs()
                    .WithBucket(fileInfo.BucketName)
                    .WithObject(fileInfo.FilePath);

                await _minioClient.RemoveObjectAsync(removeArgs, token).ConfigureAwait(false);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to remove file in minio");
                return Error.Failure("file.remove", "Fail to remove file in minio");
            }
        }

        private async Task IfBacketsNotExistCreateBucket(
            string bucketName,
            CancellationToken token)
        {
            var bucketArgs = new BucketExistsArgs().WithBucket(bucketName);

            var checkResult = await _minioClient.BucketExistsAsync(bucketArgs, token);

            if (checkResult == false)
            {
                var newBucket = new MakeBucketArgs().WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(newBucket, token);
            }
        }
    }
}
