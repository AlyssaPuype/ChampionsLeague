using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    // Data/Seeders/StadionSeeder.cs — returned de stadions
    public static class StadionSeeder
    {
        public static List<Stadion> Seed(ChampionsLeagueDbContext context)
        {
            var stadions = new List<Stadion>
        {
            new() { Naam = "Santiago Bernabéu Stadium", Capaciteit = 85000 },
            new() { Naam = "Etihad Stadium", Capaciteit = 53000 },
            new() { Naam = "Allianz Arena", Capaciteit = 75000 },
            new() { Naam = "Parc des Princes", Capaciteit = 48000 },
            new() { Naam = "Jan Breydelstadion", Capaciteit = 29000 },
            new() { Naam = "Camp Nou", Capaciteit = 99000 }
        };
            context.Stadions.AddRange(stadions);
            return stadions;
        }
    }
}