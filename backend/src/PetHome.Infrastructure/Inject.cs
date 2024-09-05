using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHome.Application.Providers;
using PetHome.Application.Volunteers;
using PetHome.Infrastructure.Options;
using PetHome.Infrastructure.Providers;
using PetHome.Infrastructure.Repositories;

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
