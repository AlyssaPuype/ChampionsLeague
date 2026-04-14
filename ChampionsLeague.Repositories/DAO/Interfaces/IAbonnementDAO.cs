using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IAbonnementDAO
    {

        Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId);
        Task<Zitplaats?> GetBeschikbareZitplaatsAsync(int clubId);
        Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId);
       

    }
}
