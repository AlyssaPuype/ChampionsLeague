using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IZitplaatsService
    {
    
        Task<IEnumerable<Zitplaats>> GetBeschikbaarPerStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);

        Task<int> GetAantalBeschikbaarAsync(int stadionvakId, int matchId);

        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId);
    }
}
