using Microsoft.Extensions.DependencyInjection;

namespace PetHome.Species.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSpeciesContracts(
            this IServiceCollection services)
        {
            services.AddScoped<ISpeciesContract>();

            return services;
        }
    }
}
