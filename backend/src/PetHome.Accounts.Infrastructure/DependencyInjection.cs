using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PetHome.Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountsInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            //services.AddIdentity<User, Role>()
            //    .AddEntityFrameworkStores<AuthorizationDbContext>();            

            services.AddScoped<AuthorizationDbContext>();

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "test",
                    ValidAudience = "test",
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(
                            "testasdsg[kfgpfhgsdfjhlskdfhglurgrunvasnvropuaphruhgapfva")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }
    }
}
