using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Shared.Core.Database;
using PetHome.Shared.Framework;
using PetHome.Species.Application;
using PetHome.Species.Application.SpeciesManagement;
using PetHome.Species.Infrastructure.DbContexts;
using PetHome.Species.Infrastructure.Repositories;

namespace PetHome.Species.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSpeciesInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<WriteDbContext>();
            services.AddScoped<IReadDbContextSpecies, ReadDbContext>();
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            return services;
        }
    }
}
