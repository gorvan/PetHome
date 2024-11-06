using Microsoft.Extensions.DependencyInjection;

namespace PetHome.Accounts.Infrastructure.Seeding
{
    public class AccountsSeeder(IServiceScopeFactory scopeFactory)
    {
        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            using var scope = scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();

            await service.SeedAsync(cancellationToken);
        }
    }
}
