using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHome.Application.FileProvider;
using PetHome.Application.Messaging;
using PetHome.Application.VolunteersManagement;
using PetHome.Infrastructure.BackgroundServices;
using PetHome.Infrastructure.MessageQueues;
using PetHome.Infrastructure.Options;
using PetHome.Infrastructure.Providers;
using PetHome.Infrastructure.Repositories;
using FileInfo = PetHome.Application.FileProvider.FileInfo;

namespace PetHome.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IVolunteerRepository, VolunteersRepository>();
            services.AddScoped<IFileProvider, MinioProvider>();
            services.AddMinio(configuration);
            services.AddHostedService<FilesCleanerBackgroundService>();
            services.AddSingleton<IMessageQueue<FileInfo>, MemoryCleanerQueue<FileInfo>>();
            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMinio(options =>
            {
                var minioOptions = configuration
                .GetSection(MinioOptions.MINIO)
                .Get<MinioOptions>() ??
                throw new ApplicationException("Missing minio configuration");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(
                    minioOptions.AccessKey,
                    minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSsl);
            });
            return services;
        }
    }
}
