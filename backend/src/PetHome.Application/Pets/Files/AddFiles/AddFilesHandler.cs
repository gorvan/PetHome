using Microsoft.Extensions.Logging;
using PetHome.Application.FileProvider;
using PetHome.Application.VolunteersManagement.PetManagement.AddPetFiles;
using PetHome.Domain.Shared;


namespace PetHome.Application.Pets.Files.AddFiles
{
    public class AddFilesHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddFilesHandler> _logger;

        public AddFilesHandler(IFileProvider fileProvider,
            ILogger<AddFilesHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string>> Execute(
           AddFileCommand command,
           CancellationToken token)
        {
            var fileData =
                new FileData(
                    command.FileStream,
                    AddPetFilesHandler.BUCKET_NAME,
                    command.FilePath);

            var result = await _fileProvider.UploadFile(fileData, token);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
