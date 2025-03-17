using Microsoft.Extensions.DependencyInjection;

namespace PetHome.Disscusions.Contracts;
public static class DependencyInjection
{
    public static IServiceCollection AddDisscusionContracts(
            this IServiceCollection services)
    {
        services.AddScoped<IDisscusionContract>();

        return services;
    }
}
