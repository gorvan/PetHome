using Microsoft.EntityFrameworkCore;
using PetHome.Accounts.Domain;

namespace PetHome.Accounts.Infrastructure.IdentityManager;

public class PermissionsManager(AccountsDbContext accountsContext)
{
    public async Task AddRangeIfNotExist(IEnumerable<string> permissions, CancellationToken cancellationToken)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await accountsContext.Permissions
                 .AnyAsync(p => p.Code == permissionCode, cancellationToken);

            if (isPermissionExist)
                return;

            await accountsContext.Permissions.AddAsync(
                new Permission { Code = permissionCode }, 
                cancellationToken);
        }

        await accountsContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Permission?> FindeByCode(string code, CancellationToken cancellationToken) =>
        await accountsContext.Permissions.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);

    public async Task<HashSet<string>> GetUserPermissions(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var permissions = await accountsContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r=>r.RolePermission)
            .Select(rp=>rp.Permission.Code)
            .ToListAsync(cancellationToken);

        return permissions.ToHashSet();
    }
}
