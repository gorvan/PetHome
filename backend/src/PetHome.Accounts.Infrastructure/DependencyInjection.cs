using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure;
using PetHome.Accounts.Infrastructure.Abstractions;
using PetHome.Accounts.Infrastructure.Authorization;
using PetHome.Accounts.Infrastructure.IdentityManager;
using PetHome.Accounts.Infrastructure.Options;
using PetHome.Accounts.Infrastructure.Providers;
using PetHome.Accounts.Infrastructure.Seeding;
using PetHome.Shared.Core.Abstractions;

namespace PetHome.Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountsInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services
                .AddTransient<ITokenProvider, JwtTokenProvider>()
                .AddDbContext()
                .AddCustomAuthorization()
                .AddJwtOptions(configuration)
                .AddJwtBearer(configuration);

            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services
                .AddScoped<AccountsDbContext>()
                .AddIdentity<User, Role>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<AccountsDbContext>()
                .AddDefaultTokenProviders();

            return services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(nameof(Accounts));
        }

        private static IServiceCollection AddJwtOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtOtions>(configuration.GetSection(JwtOtions.JWT));
            services.AddOptions<JwtOtions>();
            services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.ADMIN));
            services.Configure<RefreshSessionOptions>(configuration.GetSection(RefreshSessionOptions.SESSION_OPTIONS));            
            return services;
        }

        private static IServiceCollection AddJwtBearer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOtions.JWT).Get<JwtOtions>()
                                    ?? throw new ApplicationException("Missing jwt configuration");

                options.TokenValidationParameters = 
                    TokenValidationParametersFactory.CreateWithLifeTime(jwtOptions);
            });

            return services;
        }

        private static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissionRequirmentHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            services.AddAuthorization();

            services.AddSingleton<AccountsSeeder>();

            services.AddScoped<AccountsSeederService>();

            services.AddScoped<PermissionsManager>();

            services.AddScoped<IRefreshSessionManager, RefreshSessionManager>();

            services.AddScoped<RolePermissionManager>();

            services.AddScoped<AdminAccountManager>();

            services.AddScoped<ParticipantAccountManager>();

            return services;
        }
    }
}
