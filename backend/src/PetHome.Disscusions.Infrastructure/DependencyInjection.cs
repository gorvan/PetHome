using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Disscusions.Application.DisscusionManagement;
using PetHome.Disscusions.Infrastructure.DbContexts;
using PetHome.Disscusions.Infrastructure.Repositories;

namespace PetHome.Disscusions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddDisscusionInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddScoped<IDisscusionRepositiory, DisscusionRepositiory>()
            .AddScoped<IReadDbContextDisscusions, ReadDbContext>()
            .AddScoped<WriteDbContext>();
    }
}
