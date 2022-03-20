using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static async Task PrepPopulation(IApplicationBuilder app)
    {
        await using var serviceScope = app.ApplicationServices.CreateAsyncScope();

        await SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static async Task SeedData(AppDbContext context)
    {
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