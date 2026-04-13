using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IAbonnementDAO
    {

        Task<IEnumerable<Abonnement>> GetAllAsync();
        Task<Abonnement?> GetByIdAsync(int id);
        Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId);
        Task<Zitplaats?> GetBeschikbareZitplaatsAsync(int clubId);
        Task AddAsync(Abonnement abonnement);

        Task SaveAsync();


    }
}
