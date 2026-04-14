using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IOrderService
    {
            
        //ticket aanmaken
        Task CreateTicketOrderAsync(string userId, string email, int matchId, int zitplaatsId, int aantalTickets);

        // abonnement aanmaken
        Task CreateAbonnementOrderAsync(string userId, string email, int clubId);
    }
}
