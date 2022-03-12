using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class Seed
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedPlatforms(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedPlatforms(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                    System.Console.WriteLine("--> Applied migrations");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (context.Platforms.Any())
            {
                Console.WriteLine("--> Platforms already exists");
            }
            else
            {
                Console.WriteLine("--> Seeding platforms");
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Sql server exp", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud native", Cost = "Free" }
                    );
                context.SaveChangesAsync();
            }

        }
    }
}
