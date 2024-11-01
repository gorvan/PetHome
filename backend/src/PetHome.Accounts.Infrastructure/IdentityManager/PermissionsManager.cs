using Microsoft.EntityFrameworkCore;
using PetHome.Accounts.Domain;

namespace PetHome.Accounts.Infrastructure.IdentityManager;

public class PermissionsManager(AccountsDbContext accountsContext)
{
    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await accountsContext.Permissions
                 .AnyAsync(p => p.Code == permissionCode);

            if (isPermissionExist)
                return;

            await accountsContext.Permissions.AddAsync(new Permission { Code = permissionCode });
        }

        await accountsContext.SaveChangesAsync();
    }

    public async Task<Permission?> FindeByCode(string code) =>
        await accountsContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);

    public async Task<HashSet<string>> GetUserPermissions(Guid userId)
    {
        var permissions = await accountsContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r=>r.RolePermission)
            .Select(rp=>rp.Permission.Code)
            .ToListAsync();

        return permissions.ToHashSet();
    }
}
