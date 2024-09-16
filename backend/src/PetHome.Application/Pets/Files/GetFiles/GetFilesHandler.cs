using Microsoft.Extensions.Logging;
using PetHome.Application.FileProvider;
using PetHome.Domain.Shared;

namespace PetHome.Application.Pets.Files.GetFiles
{
    public class GetFilesHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<GetFilesHandler> _logger;

        public GetFilesHandler(IFileProvider fileProvider,
            ILogger<GetFilesHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<List<string>>> Execute(
           GetFilesCommand command,
           CancellationToken token)
        {
            var result = await _fileProvider.GetFiles(command, token);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
