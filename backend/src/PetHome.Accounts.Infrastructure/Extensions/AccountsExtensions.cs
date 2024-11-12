using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Accounts.Infrastructure.Seeding;

namespace PetHome.Accounts.Infrastructure.Extensions;

public static class AccountsExtensions
{
    public static async Task SeedAccountsData(this WebApplication app)
    {
        var seeder = app.Services.GetRequiredService<AccountsSeeder>();
        await seeder.SeedAsync();
    }
}
