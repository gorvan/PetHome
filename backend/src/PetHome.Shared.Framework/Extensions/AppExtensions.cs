using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PetHome.Shared.Core.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigrations<T>(this WebApplication app) where T : DbContext
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Npgsql.NpgsqlException ex)
        {
            var innerMessage =
                string.IsNullOrWhiteSpace(ex.InnerException?.Message)
                ? ""
                : " . " + ex.InnerException.Message;

            app.Logger.LogError("Error: {message}",
                $"{ex.Source} . {ex.Message}{innerMessage}");
        }
    }
}
