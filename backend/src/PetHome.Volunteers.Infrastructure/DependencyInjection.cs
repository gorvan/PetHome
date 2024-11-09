using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetHome.Shared.Core.Database;
using PetHome.Shared.Core.FileProvider;
using PetHome.Shared.Core.Messaging;
using PetHome.Shared.Framework;
using PetHome.Shared.Framework.BackgroundServices;
using PetHome.Shared.Framework.MessageQueues;
using PetHome.Shared.Framework.Providers;
using PetHome.Volunteers.Application;
using PetHome.Volunteers.Application.VolunteersManagement;
using PetHome.Volunteers.Infrastructure.DbContexts;
using PetHome.Volunteers.Infrastructure.Repositories;
using FileInfo = PetHome.Shared.Core.FileProvider.FileInfo;
using MinioOptions = PetHome.Shared.Framework.Options.MinioOptions;


namespace PetHome.Volunteers.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVolunteerInfrastructure(
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
            services.AddScoped<IReadDbContextVolunteers, ReadDbContext>();
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MinioOptions>(
                configuration.GetSection(MinioOptions.MINIO));

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
