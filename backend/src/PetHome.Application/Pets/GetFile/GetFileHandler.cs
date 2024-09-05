using Microsoft.Extensions.Logging;
using PetHome.Application.Providers;
using PetHome.Domain.Shared;

namespace PetHome.Application.Pets.GetFile
{
    public class GetFileHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<GetFileHandler> _logger;

        public GetFileHandler(IFileProvider fileProvider,
            ILogger<GetFileHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string>> Execute(
           GetFileCommand command,
           CancellationToken token)
        {
            var result = await _fileProvider.Get(command);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
