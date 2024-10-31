using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetHome.Accounts.Domain;
using PetHome.Accounts.Domain.Accounts;
using PetHome.Accounts.Infrastructure.IdentityManager;
using PetHome.Accounts.Infrastructure.Options;
using PetHome.Shared.Core.Shared;
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
        IOptions<AdminOptions> adminOptions,
        ILogger<AccountsSeeder> logger)
    {
        private AdminOptions _adminOptions = adminOptions.Value;

        public async Task SeedAsync()
        {
            var json = await File.ReadAllTextAsync(FilePath.Accounts);

            var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                ?? throw new ApplicationException("Could not deserialize role permission config");

            await SeedPermissions(seedData);

            await SeedRoles(seedData);

            await SeedRolePermissions(seedData);

            var adminRole = await roleManager.FindByNameAsync(AdminAccount.ADMIN)
                ?? throw new ApplicationException("Could not find admin role.");

            var adminUser = User.CreateAmin(
                _adminOptions.UserName,
                _adminOptions.Email,
                adminRole);            

            await userManager.CreateAsync(adminUser, _adminOptions.Password);

            var fullName = FullName.Create(
                _adminOptions.UserName,
                _adminOptions.UserName, 
                _adminOptions.UserName)
                .Value; 

            var adminAccount = new AdminAccount(fullName, adminUser);

            await adminAccountManager.CreateAdminAccount(adminAccount);
        }

        private async Task SeedRolePermissions(
            RolePermissionOptions seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                var rolePermissions = seedData.Roles[roleName];

                await rolePermissionManager.AddRangeIfExist(role!.Id, rolePermissions);
            }
        }

        private async Task SeedPermissions(RolePermissionOptions seedData)
        {
            var permissionsToAdd = seedData.Permissions.SelectMany(pg => pg.Value);

            await permissionManager.AddRangeIfExist(permissionsToAdd);

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
    }
}
