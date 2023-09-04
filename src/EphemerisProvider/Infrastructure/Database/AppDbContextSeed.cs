using Microsoft.EntityFrameworkCore;

namespace EphemerisProvider.Infrastructure.Database;

public static class AppDbContextSeed
{
    public static async Task SeedAppDbContext(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        try
        {
            await db.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
            logger.LogError(ex, "An error occurred while seeding the database.");

            throw;
        }
    }
}