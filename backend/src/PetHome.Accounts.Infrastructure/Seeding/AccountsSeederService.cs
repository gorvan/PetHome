using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Accounts.Infrastructure.IdentityManager;
using PetHome.Accounts.Infrastructure.Options;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Framework;
using PetHome.Shared.SharedKernel;
using System.Text.Json;

namespace PetHome.Accounts.Infrastructure.Seeding
{
    public class AccountsSeederService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        AdminAccountManager adminAccountManager,
        PermissionsManager permissionManager,
        RolePermissionManager rolePermissionManager,
        [FromServices] IUnitOfWork unitOfWork,
        IOptions<AdminOptions> adminOptions,
        ILogger<AccountsSeeder> logger)
    {
        private AdminOptions _adminOptions = adminOptions.Value;

        public async Task SeedAsync(CancellationToken cancellationToken)
        {
            var json = await File.ReadAllTextAsync(FilePath.Accounts);

            var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                ?? throw new ApplicationException("Could not deserialize role permission config");

            await SeedPermissions(seedData, cancellationToken);

            await SeedRoles(seedData);

            await SeedRolePermissions(seedData);

            await SeedAdmin(cancellationToken);
        }

        private async Task SeedRolePermissions(
            RolePermissionOptions seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                var rolePermissions = seedData.Roles[roleName];

                await rolePermissionManager.AddRangeIfNotExist(role!.Id, rolePermissions);
            }
        }

        private async Task SeedPermissions(RolePermissionOptions seedData, CancellationToken cancellationToken)
        {
            var permissionsToAdd = seedData.Permissions.SelectMany(pg => pg.Value);

            await permissionManager.AddRangeIfNotExist(permissionsToAdd, cancellationToken);

            logger.LogInformation("Permissions added to database");
        }

        private async Task SeedRoles(RolePermissionOptions seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    await roleManager.CreateAsync(new Role { Name = roleName });
                }
            }

            logger.LogInformation("Roles added to database");
        }

        private async Task SeedAdmin(CancellationToken cancellationToken)
        {
            var isAdminExist = await adminAccountManager.IsAdminAccountExist(
                _adminOptions.UserName,
                _adminOptions.Email);

            if (isAdminExist)
                return;

            var adminRole = await roleManager.FindByNameAsync(AdminAccount.ADMIN)
                ?? throw new ApplicationException("Could not find admin role.");

            var adminUser = User.CreateAmin(
                _adminOptions.UserName,
                _adminOptions.Email,
                adminRole);

            var transaction = await unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var result = await userManager.CreateAsync(adminUser, _adminOptions.Password);

                if (result.Succeeded is false)
                {
                    await userManager.DeleteAsync(adminUser);
                    transaction.Rollback();
                    return;
                }

                var fullName = FullName.Create(
                    _adminOptions.UserName,
                    _adminOptions.UserName,
                    _adminOptions.UserName)
                    .Value;

                var adminAccount = new AdminAccount(fullName, adminUser);

                await adminAccountManager.CreateAdminAccount(adminAccount);

                await unitOfWork.SaveChangesAsync(cancellationToken);
                transaction.Commit();

                logger.LogInformation("Admin added to database");
            }
            catch (Exception ex)
            {
                await userManager.DeleteAsync(adminUser);
                transaction.Rollback();

                logger.LogError($"Fail to seed admin: {ex.Message}");
            }
        }
    }
}
