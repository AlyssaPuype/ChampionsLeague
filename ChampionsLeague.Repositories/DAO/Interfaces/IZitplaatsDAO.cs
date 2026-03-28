using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
        Task<IEnumerable<Zitplaats>> GetAllAsync();
        Task<Zitplaats?> GetByIdAsync(int id);
         Task<IEnumerable<Zitplaats>> GetByStadionvakAsync(int stadionvakId, int matchId, int aantalGewensteZitplaatsen);
    }
}
