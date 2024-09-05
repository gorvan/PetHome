using Microsoft.Extensions.Logging;
using PetHome.Application.Providers;
using PetHome.Domain.Shared;

namespace PetHome.Application.Pets.Delete
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
            var result = await _fileProvider.Delete(command, token);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
