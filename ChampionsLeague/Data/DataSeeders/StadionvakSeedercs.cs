using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    
    public static class StadionvakSeeder
    {
        public static List<Stadionvak> Seed(ChampionsLeagueDbContext context, List<Stadion> stadions)
        {
            var stadionvakken = new List<Stadionvak>();

            //Voor elk stadion, 8 vakken
            foreach (var stadion in stadions)
            {
                int capaciteitPerVak = stadion.Capaciteit / 8;

                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Achter doel - Thuis", Code = "ODT", Ring = "Onder", Type = "Achterdoel", Partij = "Thuis", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Achter doel - Bezoek", Code = "ODB", Ring = "Onder", Type = "Achterdoel", Partij = "Bezoek", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Zijlijn oost - Neutraal", Code = "OZON", Ring = "Onder", Type = "Zijlijn", Partij = "Oost", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Zijlijn west - Neutraal", Code = "OZWN", Ring = "Onder", Type = "Zijlijn", Partij = "West", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Achter doel - Thuis", Code = "BDT", Ring = "Boven", Type = "Achterdoel", Partij = "Thuis", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Achter doel - Bezoek", Code = "BDB", Ring = "Boven", Type = "Achterdoel", Partij = "Bezoek", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Zijlijn oost - Neutraal", Code = "BZON", Ring = "Boven", Type = "Zijlijn", Partij = "Oost", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Zijlijn west - Neutraal", Code = "BZWN", Ring = "Boven", Type = "Zijlijn", Partij = "West", Capaciteit = capaciteitPerVak });
            }
            
            context.Stadionvakken.AddRange(stadionvakken);
            return stadionvakken;
        }
    }
}