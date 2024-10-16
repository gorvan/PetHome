using Microsoft.Extensions.DependencyInjection;

namespace PetHome.Volunteers.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVolunteersContracts(
            this IServiceCollection services)
        {
            services.AddScoped<IVolunteersContract>();

            return services;
        }
    }
}
