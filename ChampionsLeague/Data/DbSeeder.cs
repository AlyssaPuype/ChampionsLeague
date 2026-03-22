
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

            if (context.Clubs.Any())
            {
                return;
            }

            var stadions = StadionSeeder.Seed(context);
            var clubs = ClubSeeder.Seed(context, stadions);
            var stadionvakken = StadionvakSeeder.Seed(context, stadions);
            var zitplaatsen = ZitplaatsSeeder.Seed(context, stadionvakken);
            var competities = CompetitieSeeder.Seed(context);
            var matches = MatchSeeder.Seed(context, clubs, competities);

            await context.SaveChangesAsync();

        }
    }
}