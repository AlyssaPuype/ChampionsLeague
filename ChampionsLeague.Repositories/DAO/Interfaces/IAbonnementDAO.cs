using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IAbonnementDAO
    {

        Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId);
        Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId);

    }
}
