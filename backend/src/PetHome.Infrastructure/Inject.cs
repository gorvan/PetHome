using Microsoft.Extensions.DependencyInjection;
using PetHome.Application.Volunteers;
using PetHome.Infrastructure.Repositories;

namespace PetHome.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IVolunteerRepository, VolunteersRepository>();
            return services;
        }
    }
}
