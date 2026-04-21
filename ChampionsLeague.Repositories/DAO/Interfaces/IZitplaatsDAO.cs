using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
               
        //Tickets: Lijst van vrije zitplaatsen in een vak per match
        Task<IEnumerable<Zitplaats>> GetBeschikbareZitplaatsenVoorTicketAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);

        //Abonnementen: Eerste vrije zitplaats in een vak
        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int stadionvakId);

        //Get aantal vrije zitplaatsen
        Task<int> GetAantalBeschikbareZitplaatsen(int stadionvakId, int matchId);



    }


}
