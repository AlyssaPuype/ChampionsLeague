using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    public static class StadionSeeder
    {
        public static List<Stadion> Seed(ChampionsLeagueDbContext context)
        {
            var stadions = new List<Stadion>
        {
            new() { Naam = "Santiago Bernabéu Stadium", Capaciteit = 85000, Stad = "Madrid" },
            new() { Naam = "Etihad Stadium", Capaciteit = 53000, Stad = "Manchester" },
            new() { Naam = "Allianz Arena", Capaciteit = 75000, Stad = "Munich" },
            new() { Naam = "Parc des Princes", Capaciteit = 48000, Stad = "Paris" },
            new() { Naam = "Jan Breydelstadion", Capaciteit = 29000, Stad = "Bruges" },
            new() { Naam = "Camp Nou", Capaciteit = 99000, Stad = "Barcelona" }
        };
            context.Stadions.AddRange(stadions);
            return stadions;
        }
    }
}