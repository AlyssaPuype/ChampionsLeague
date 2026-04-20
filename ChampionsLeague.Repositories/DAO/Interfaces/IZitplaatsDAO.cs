using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
               
        //Geen overboeking mogelijk maken
        Task<IEnumerable<Zitplaats>> GetBeschikbaarPerStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);


        //krijg het aantal beschikbare zitplaatsen voor een bepaalde stadionvak en match
        Task<int> GetAantalBeschikbaarAsync(int stadionvakId, int matchId);

        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId, int stadionvakId);


    }


}
