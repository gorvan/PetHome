﻿using Microsoft.Extensions.Logging;
using PetHome.Application.Pets.GetFile;
using PetHome.Application.Providers;
using PetHome.Domain.Shared;

namespace PetHome.Application.Pets.GetFiles
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
            var result = await _fileProvider.Get(command, token);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}