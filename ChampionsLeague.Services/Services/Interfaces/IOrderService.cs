using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserAsync(string userId);
        
        //ticket aanmaken
        Task CreateTicketOrderAsync(string userId, string email, int matchId, int zitplaatsId, int aantalTickets);

        // abonnement aanmaken
        Task CreateAbonnementOrderAsync(string userId, string email, int clubId);
    }
}
