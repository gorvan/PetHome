using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Shared.Core.Abstractions;
using System.Reflection;

namespace PetHome.Accounts.Application
{
    public static class DependencyInjection
    {
        private static Assembly assembly = typeof(DependencyInjection).Assembly;

        public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
        {
            services
                .AddCommands()
                .AddQueries()
                .AddValidatorsFromAssembly(assembly);

            return services;
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services.Scan(scan => scan.FromAssemblies(assembly)
                .AddClasses(classes => classes
                .AssignableToAny([typeof(ICommandHandler<,>), typeof(ICommandHandler<>)]))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.Scan(scan => scan.FromAssemblies(assembly)
                .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
        }
    }
}
