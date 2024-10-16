using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Core.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<string>> UploadFile(
            FileData fileData,
            CancellationToken token);

        Task<Result<string>> GetFile(
            FileInfo fileInfo);

        Task<Result<List<string>>> GetFiles(
            FileInfo fileInfo,
            CancellationToken token);

        Task<Result> RemoveFile(
            FileInfo fileInfo,
            CancellationToken token);
    }
}
