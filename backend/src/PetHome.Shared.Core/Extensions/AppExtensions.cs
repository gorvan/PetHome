using Microsoft.Graph.Models;

namespace PetHome.Shared.Core.Extensions
{
    public static class AppExtensions
    {
        //public static async Task ApplyMigrations(this WebApplication app)
        //{
        //    await using var scope = app.Services.CreateAsyncScope();

        //    var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        //    try
        //    {
        //        await dbContext.Database.MigrateAsync();
        //    }
        //    catch (Npgsql.NpgsqlException ex)
        //    {
        //        var innerMessage =
        //            string.IsNullOrWhiteSpace(ex.InnerException?.Message)
        //            ? ""
        //            : " . " + ex.InnerException.Message;

        //        app.Logger.LogError("Error: {message}",
        //            $"{ex.Source} . {ex.Message}{innerMessage}");
        //    }
        //}
    }
}
