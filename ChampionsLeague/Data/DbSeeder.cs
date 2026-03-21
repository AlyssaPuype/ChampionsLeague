
using ChampionsLeague.Data.DataSeeders;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChampionsLeagueDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await context.Database.MigrateAsync();
            Console.WriteLine(">>> MigrateAsync done");



            if (context.Clubs.Any())
            {
                Console.WriteLine(">>> Already seeded, skipping");
                return;
            }

            var stadions = StadionSeeder.Seed(context);
            var clubs = ClubSeeder.Seed(context, stadions);
            await context.SaveChangesAsync();

    

        }
    }
}