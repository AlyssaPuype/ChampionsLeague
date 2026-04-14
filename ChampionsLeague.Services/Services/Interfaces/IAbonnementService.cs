using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IAbonnementService
    {
       
        Task CreateAbonnementOrderAsync(string userId, string email, int clubId);
        Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId);
        Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId);


    }
}