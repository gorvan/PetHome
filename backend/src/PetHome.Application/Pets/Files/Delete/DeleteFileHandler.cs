using Microsoft.Extensions.Logging;
using PetHome.Application.FileProvider;
using PetHome.Domain.Shared;

namespace PetHome.Application.Pets.Files.Delete
{
    public class DeleteFileHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<DeleteFileHandler> _logger;

        public DeleteFileHandler(IFileProvider fileProvider,
            ILogger<DeleteFileHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result> Execute(
           DeleteFileCommand command,
           CancellationToken token)
        {
            var result = await _fileProvider.DeleteFile(command, token);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
