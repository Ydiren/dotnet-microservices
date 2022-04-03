using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static async Task PrepPopulation(IApplicationBuilder app, bool isProduction)
    {
        await using var serviceScope = app.ApplicationServices.CreateAsyncScope();

        await SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
    }

    private static async Task SeedData(AppDbContext context, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not run migrations: {e.Message}");
            }
        } 
        
        if (context.Platforms.Any())
        {
            Console.WriteLine("--> We already have data.");
            return;
        }

        Console.WriteLine("--> Seeding data...");

        await context.Platforms.AddRangeAsync(new Platform
                                   {
                                       Name = "Dot Net",
                                       Publisher = "Microsoft",
                                       Cost = "Free"
                                   },
                                   new Platform
                                   {
                                       Name = "SQL Server Express",
                                       Publisher = "Microsoft",
                                       Cost = "Free"
                                   },
                                   new Platform
                                   {
                                       Name = "Kubernetes",
                                       Publisher = "Cloud Native Computing Foundation",
                                       Cost = "Free"
                                   });

        await context.SaveChangesAsync();
    }
}