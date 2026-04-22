using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using ChampionsLeague.Util.Mail.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services
{
    public class AbonnementService : IAbonnementService
    {

        private readonly IAbonnementDAO _abonnementDAO;



        public AbonnementService(IAbonnementDAO abonnementDAO)
        {
            _abonnementDAO = abonnementDAO;
        }

      
        public async Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId)
        {
            return await _abonnementDAO.GetByUserIdAsync(userId);
        }

        public async Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId)
        {
            return await _abonnementDAO.HeeftAbonnementVoorClubAsync(userId, clubId);
        }

    }
}
