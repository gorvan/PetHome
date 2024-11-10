using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Framework;
using PetHome.Volunteers.Infrastructure.Services;

namespace PetHome.Volunteers.Infrastructure.BackgroundServices
{
    public class SoftDeleteBackgroundService(
        ILogger<SoftDeleteBackgroundService> logger, 
        IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("SoftDeleteBackgroundService is started");

            while (cancellationToken.IsCancellationRequested)
            {
                await using var scope = scopeFactory.CreateAsyncScope();

                var deleteService = scope.ServiceProvider.GetRequiredService<DeleteExpiredEntitiesService>();

                logger.LogInformation("SoftDeleteBackgroundService is working");

                await deleteService.Execute(cancellationToken);

                await Task.Delay(TimeSpan.FromHours(Constants.DELETE_EXPIRED_ENTITIES_HOURS));
            }
        }
    }
}
