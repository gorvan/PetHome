using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain;
using PetHome.Shared.Framework;

namespace PetHome.Accounts.Infrastructure
{
    public class AuthorizationDbContext(IConfiguration configuration) 
        : IdentityDbContext<User, Role, Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");

            builder.Entity<Role>().ToTable("roles");

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("user_claims");

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("user_tokens");

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("user_logins");

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("user_roles");

            base.OnModelCreating(builder);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
