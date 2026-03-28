using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IZitplaatsService
    {
        Task<IEnumerable<Zitplaats>> GetAllAsync();
        Task<Zitplaats?> GetByIdAsync(int id);
        Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int stadionvakId, int matchId, int aantalGewensteZitplaatsen);
    }
}
