using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetHome.Accounts.Application.Abstractions;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Infrastructure.Providers;
using System.Text;

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
                .AddJwtOptions(configuration)
                .AddDbContext()
                .AddJwtBearer(configuration);

            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

            return services.AddScoped<AuthorizationDbContext>();
        }

        private static IServiceCollection AddJwtOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtOtions>(configuration.GetSection(JwtOtions.JWT));
            services.AddOptions<JwtOtions>();

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
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }
    }
}
