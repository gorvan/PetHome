using PetHome.Application.Pets.Files.Delete;
using PetHome.Application.Pets.Files.GetFile;
using PetHome.Application.Pets.Files.GetFiles;
using PetHome.Domain.Shared;

namespace PetHome.Application.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<string>> UploadFile(
            FileData fileData,
            CancellationToken token);

        Task<Result<string>> GetFile(
            GetFileCommand fileData);

        Task<Result<List<string>>> GetFiles(
            GetFilesCommand fileData,
            CancellationToken token);

        Task<Result<bool>> DeleteFile(
            DeleteFileCommand fileData,
            CancellationToken token);
    }
}
