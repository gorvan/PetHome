using PetHome.Application.FileProvider;
using PetHome.Application.Pets.Delete;
using PetHome.Application.Pets.GetFile;
using PetHome.Application.Pets.GetFiles;
using PetHome.Domain.Shared;

namespace PetHome.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string>> Upload(
            FileData fileData, 
            CancellationToken token);

        Task<Result<string>> Get(
            GetFileCommand fileData);

        Task<Result<List<string>>> Get(
            GetFilesCommand fileData,
            CancellationToken token);

        Task<Result<bool>> Delete(
            DeleteFileCommand fileData, 
            CancellationToken token);
    }
}
