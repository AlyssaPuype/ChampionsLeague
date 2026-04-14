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
                new() { Naam = "UEFA Champions League", StartDatum = new DateOnly(2026, 4, 30), EindDatum = new DateOnly(2026, 7, 4) }
            };

            context.Competities.AddRange(competities);
            return competities;
        }
    }
}