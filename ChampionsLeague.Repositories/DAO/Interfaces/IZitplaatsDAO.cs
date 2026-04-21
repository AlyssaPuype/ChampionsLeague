using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IZitplaatsDAO
    {
               
        //Tickets
        //Gebruiker kiest een match, een vak en het aantal gewenste zitplaatsen
        //Return: Lijst van zitplaatsen
        Task<IEnumerable<Zitplaats>> GetBeschikbareZitplaatsenVoorTicketAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen);

        //Abonnementen
        //Gebruiker kiest een vak en krijgt de eerste vrije zitplaats in dat vak toegewezen
        //Return: 1 zitplaats
        Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int stadionvakId);

        //Get aantal vrije zitplaatsen
        //Return: int
        Task<int> GetAantalBeschikbareZitplaatsen(int stadionvakId, int matchId);



    }


}
