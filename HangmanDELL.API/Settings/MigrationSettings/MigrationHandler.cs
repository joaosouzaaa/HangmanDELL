using HangmanDELL.API.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HangmanDELL.API.Settings.MigrationSettings;

public static class MigrationHandler
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<HangmanDbContext>();

        try
        {
            dbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
