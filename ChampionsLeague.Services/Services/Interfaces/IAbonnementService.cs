using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IAbonnementService
    {
       
        Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId);
        Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId);


    }
}