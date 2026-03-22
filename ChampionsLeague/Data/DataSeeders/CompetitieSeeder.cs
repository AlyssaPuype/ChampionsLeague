using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    public static class CompetitieSeeder
    {
        public static List<Competitie> Seed(ChampionsLeagueDbContext context)
        {
            var competities = new List<Competitie>
            {
                new() { Naam = "UEFA Champions League" }
            };

            context.Competities.AddRange(competities);
            return competities;
        }
    }
}