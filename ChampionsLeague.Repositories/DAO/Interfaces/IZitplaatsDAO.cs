using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
               
        //Geen overboeking mogelijk maken
        //Volledige zitplaats object ophalen om tickets te kunnen aankopen
        Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);

        //Zitplaats zoeken voor abonnement (1 zitplaats per abonnement)
        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId);


    }
}
