using Microsoft.Extensions.DependencyInjection;
using PetHome.Shared.Core.Abstractions;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement;
using PetHome.VolunteerRequests.Infrastructure.DbContexts;
using PetHome.VolunteerRequests.Infrastructure.Repositories;

namespace PetHome.VolunteerRequests.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRequestRepository, VolunteerRequestRepository>();
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IreadDbContextVolunteerRequest, ReadDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
