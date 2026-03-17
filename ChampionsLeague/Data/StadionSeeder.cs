using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data
{
    public static class StadionSeeder
    {
        public static void Seed(ChampionsLeagueDbContext context)
        {
            if (context.Stadions.Any()) return;

            context.Stadions.AddRange(
                new Stadion { Naam = "Santiago Bernabéu Stadium", Capaciteit = 85000 },
                new Stadion { Naam = "Etihad Stadium", Capaciteit = 53000 },
                new Stadion { Naam = "Allianz Arena", Capaciteit = 75000 },
                new Stadion { Naam = "Parc des Princes", Capaciteit = 48000 },
                new Stadion { Naam = "Jan Breydelstadion", Capaciteit = 29000 },
                new Stadion { Naam = "Camp Nou", Capaciteit = 99000 }
            );
            context.SaveChanges();
        }
    }
}