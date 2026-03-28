using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    public static class ZitplaatsSeeder
    {
        public static List<Zitplaats> Seed(ChampionsLeagueDbContext context, List<Stadionvak> stadionvakken)
        {
            var zitplaatsen = new List<Zitplaats>();

            // Elke zitplaats krijgt een unieke code
            foreach (var vak in stadionvakken)
            {
              
                for (int i = 1; i <= vak.Capaciteit; i++)
                {
                    var zitplaats = new Zitplaats { ZitplaatsNummer = $"{vak.Code}{i}", Stadionvak = vak};
                    zitplaatsen.Add(zitplaats);
                }
            }

            context.Zitplaatsen.AddRange(zitplaatsen);
            return zitplaatsen;
        }
    }
}
