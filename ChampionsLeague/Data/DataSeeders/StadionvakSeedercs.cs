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

                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Achter doel - Thuis", Ring = "Onder", Type = "Achterdoel", Partij = "Thuis", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Achter doel - Bezoek", Ring = "Onder", Type = "Achterdoel", Partij = "Bezoek", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Zijlijn oost - Neutraal", Ring = "Onder", Type = "Zijlijn", Partij = "Neutraal", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Onderste ring - Zijlijn west - Neutraal", Ring = "Onder", Type = "Zijlijn", Partij = "Neutraal", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Achter doel - Thuis", Ring = "Boven", Type = "Achterdoel", Partij = "Thuis", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Achter doel - Bezoek", Ring = "Boven", Type = "Achterdoel", Partij = "Bezoek", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Zijlijn- Neutraal", Ring = "Boven", Type = "Zijlijn", Partij = "Neutraal", Capaciteit = capaciteitPerVak });
                stadionvakken.Add(new Stadionvak() { Stadion = stadion, Naam = "Bovenste ring - Zijlijn - Neutraal", Ring = "Boven", Type = "Zijlijn", Partij = "Neutraal", Capaciteit = capaciteitPerVak });
        }
            
            context.Stadionvaks.AddRange(stadionvakken);
            return stadionvakken;
        }
    }
}