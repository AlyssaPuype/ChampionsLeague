
using ChampionsLeague.Data.DataSeeders;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Data
{
    public class DbSeeder
    {
        private readonly ChampionsLeagueDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbSeeder(ChampionsLeagueDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            //Commenteer uit wanneer problemen met migraties en inladen van database (zie readme)
            await _context.Database.MigrateAsync();

            //Check of data in db zit. Als er data zit, stop
            if (_context.Clubs.Any())
            {
                return;
            }

            var stadions = StadionSeeder.Seed(_context);
            var clubs = ClubSeeder.Seed(_context, stadions);
            var stadionvakken = StadionvakSeeder.Seed(_context, stadions);
            var zitplaatsen = ZitplaatsSeeder.Seed(_context, stadionvakken);
            var competities = CompetitieSeeder.Seed(_context);
            var matches = MatchSeeder.Seed(_context, clubs, competities);

            await _context.SaveChangesAsync();

        }
    }
}