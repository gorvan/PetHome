using Microsoft.Extensions.Logging;
using PetHome.Application.FileProvider;
using PetHome.Application.Providers;
using PetHome.Domain.Shared;


namespace PetHome.Application.Pets.AddFiles
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
                    command.FileStraem,
                    command.BucketName,
                    command.FilePath);

            var result = await _fileProvider.Upload(fileData, token);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
