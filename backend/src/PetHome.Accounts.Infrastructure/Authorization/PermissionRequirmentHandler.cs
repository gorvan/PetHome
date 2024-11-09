using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Accounts.Infrastructure.IdentityManager;
using PetHome.Accounts.Infrastructure.Models;
using PetHome.Shared.SharedKernel.Authorization;

namespace PetHome.Accounts.Infrastructure.Authorization
{
    public class PermissionRequirmentHandler : AuthorizationHandler<PermissionAttribute>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public PermissionRequirmentHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAttribute permission)
        {
            using var scope = _scopeFactory.CreateScope();

            var permissionsManager = scope.ServiceProvider.GetRequiredService<PermissionsManager>();

            var userIdString = context.User.Claims
                .FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                context.Fail();
                return;
            }

            var permissions = await permissionsManager.GetUserPermissions(userId);

            if (permissions.Contains(permission.Code))
            {
                context.Succeed(permission);
                return;
            }

            context.Fail();
        }
    }
}
