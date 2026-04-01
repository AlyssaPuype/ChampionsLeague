using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
        Task<IEnumerable<Zitplaats>> GetAllZitplaatsenAsync();
        Task<Zitplaats?> GetByIdAsync(int id);
        
        //Geen overboeking mogelijk maken
        Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);
    }
}
