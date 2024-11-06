using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;
using System.Text.Json;
using Constants = PetHome.Shared.Framework.Constants;

namespace PetHome.Accounts.Infrastructure
{
    public class AccountsDbContext(IConfiguration configuration)
        : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<Permission> Permissions => Set<Permission>();

        public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();
        public DbSet<ParticipantAccount> ParticipantAccounts => Set<ParticipantAccount>();
        public DbSet<RefreshSession> RefreshSessions => Set<RefreshSession>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("users");

            builder.Entity<User>()
                .Property(u => u.SocialNetworks)
               .HasConversion(
                   s => JsonSerializer.Serialize(s, JsonSerializerOptions.Default),
                   json => JsonSerializer.Deserialize<List<SocialNetwork>>(
                       json,
                       JsonSerializerOptions.Default)!);

            builder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<IdentityUserRole<Guid>>();

            builder.Entity<AdminAccount>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<AdminAccount>(a => a.UserId);

            builder.Entity<AdminAccount>()
                .ComplexProperty(v => v.FullName,
                fb =>
                {
                    fb.Property(n => n.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name");

                    fb.Property(n => n.SecondName)
                    .IsRequired()
                    .HasColumnName("second_name");

                    fb.Property(n => n.Surname)
                    .IsRequired()
                    .HasColumnName("surname");
                });

            builder.Entity<ParticipantAccount>()
                .HasOne(pa => pa.User)
                .WithOne()
                .HasForeignKey<ParticipantAccount>(pa => pa.UserId);

            builder.Entity<ParticipantAccount>()
                .ComplexProperty(pa => pa.FullName,
                fb =>
                {
                    fb.Property(n => n.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name");

                    fb.Property(n => n.SecondName)
                    .IsRequired()
                    .HasColumnName("second_name");

                    fb.Property(n => n.Surname)
                    .IsRequired()
                    .HasColumnName("surname");
                });

            builder.Entity<VolunteerAccount>()
                .HasOne(va => va.User)
                .WithOne()
                .HasForeignKey<VolunteerAccount>(va => va.UserId);

            builder.Entity<VolunteerAccount>()
                .ComplexProperty(va => va.FullName,
                fb =>
                {
                    fb.Property(n => n.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name");

                    fb.Property(n => n.SecondName)
                    .IsRequired()
                    .HasColumnName("second_name");

                    fb.Property(n => n.Surname)
                    .IsRequired()
                    .HasColumnName("surname");
                });

            builder.Entity<VolunteerAccount>()
                .Property(va => va.Expirience)
                .HasColumnName("expirience");

            builder.Entity<VolunteerAccount>()
                .Property(va => va.Sertificates)
                .HasConversion(
                    s => JsonSerializer.Serialize(
                        s.Select(x => x.DescriptionValue),
                        JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<List<string>>(json, JsonSerializerOptions.Default)!
                        .Select(str => Description.Create(str).Value).ToList())
                .HasColumnName("sertificates");

            builder.Entity<VolunteerAccount>()
                .Property(va => va.Requisites)
                .HasConversion(
                    r => JsonSerializer.Serialize(
                        r.Select(x =>
                            new RequisiteDto(x.Name, x.Description)),
                            JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!
                        .Select(dto => Requisite.Create(dto.Name, dto.Description).Value).ToList())
                .HasColumnName("requisites");

            builder.Entity<Role>()
                .ToTable("roles");

            builder.Entity<RefreshSession>()
                .ToTable("refresh_sessions");

            builder.Entity<RefreshSession>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r=>r.UserId);

            builder.Entity<Permission>()
                .ToTable("permissions");

            builder.Entity<Permission>()
                .HasIndex(p => p.Code)
                .IsUnique();

            builder.Entity<Permission>()
                .HasIndex(p => p.Code)
                .IsUnique();

            builder.Entity<Permission>()
                .Property(p => p.Description)
                .HasMaxLength(300);

            builder.Entity<RolePermission>()
                .ToTable("role_permissions");

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermission)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("user_claims");

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("user_tokens");

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("user_logins");

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("user_roles");

            builder.HasDefaultSchema("accounts");

            base.OnModelCreating(builder);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
