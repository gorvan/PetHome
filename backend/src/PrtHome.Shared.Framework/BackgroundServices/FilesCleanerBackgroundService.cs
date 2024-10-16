using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.FileProvider;
using PetHome.Shared.Core.Messaging;
using FileInfo = PetHome.Shared.Core.FileProvider.FileInfo;


namespace PetHome.Shared.Framework.BackgroundServices
{
    public class FilesCleanerBackgroundService : BackgroundService
    {
        private readonly ILogger<FilesCleanerBackgroundService> _logger;
        private readonly IMessageQueue<FileInfo> _messageQueue;
        private readonly IServiceScopeFactory _scopeFactory;

        public FilesCleanerBackgroundService(
            IMessageQueue<FileInfo> messageQueue,
            ILogger<FilesCleanerBackgroundService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _messageQueue = messageQueue;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

            while (stoppingToken.IsCancellationRequested == false)
            {
                var fileInfos = await _messageQueue.ReadAsync(stoppingToken);
                foreach (var item in fileInfos)
                {
                    await fileProvider.RemoveFile(item, stoppingToken);
                }
            }

            _logger.LogInformation("FileCleanerBackgroundService is Started");
        }
    }
}
