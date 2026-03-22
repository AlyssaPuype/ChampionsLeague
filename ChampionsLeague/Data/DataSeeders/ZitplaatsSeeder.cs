using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    public static class ZitplaatsSeeder
    {
        public static List<Zitplaats> Seed(ChampionsLeagueDbContext context, List<Stadionvak> stadionvakken)
        {
            var zitplaatsen = new List<Zitplaats>();


            context.Zitplaats.AddRange(zitplaatsen);
            return zitplaatsen;
        }
    }
}
