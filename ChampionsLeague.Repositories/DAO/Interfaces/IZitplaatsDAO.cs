using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
               
        //Tickets: Lijst van vrije zitplaatsen in een vak per match
        //Gebruiker kiest een match, een vak en het aantal gewenste zitplaatsen
        Task<IEnumerable<Zitplaats>> GetBeschikbareZitplaatsenVoorTicketAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);

        //Abonnementen: Eerste vrije zitplaats in een vak
        //Gebruiker kiest een vak en krijgt de eerste vrije zitplaats in dat vak toegewezen
        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int stadionvakId);

        //Get aantal vrije zitplaatsen
        Task<int> GetAantalBeschikbareZitplaatsen(int stadionvakId, int matchId);



    }


}
