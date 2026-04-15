using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IZitplaatsService
    {
    
        Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);

        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId);

    }
}
