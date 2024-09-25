using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHome.Application.Database;
using PetHome.Application.FileProvider;
using PetHome.Application.Messaging;
using PetHome.Application.VolunteersManagement;
using PetHome.Infrastructure.BackgroundServices;
using PetHome.Infrastructure.DbContexts;
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
            return services.AddDbContexts()
                .AddMinio(configuration)
                .AddRepositories()
                .AddHostedServices()
                .AddMessaging();
        }

        private static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageQueue<FileInfo>, MemoryCleanerQueue<FileInfo>>();

            return services;
        }

        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<FilesCleanerBackgroundService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVolunteerRepository, VolunteersRepository>();

            return services;
        }

        private static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
            services.AddScoped<WriteDbContext>();
            services.AddScoped<IReadDbContext, ReadDbContext>();

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
            services.AddScoped<IFileProvider, MinioProvider>();
            return services;
        }
    }
}
