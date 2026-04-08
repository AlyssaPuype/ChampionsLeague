using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services
{
    public class AbonnementService : IAbonnementDAO
    {

        private readonly IAbonnementDAO _abonnementDAO;

        public AbonnementService(IAbonnementDAO abonnementDAO)
        {
            _abonnementDAO = abonnementDAO;
        }

        public async Task<IEnumerable<Abonnement>> GetAllAsync()
        {
            return await _abonnementDAO.GetAllAsync();
        }

        public async Task<Abonnement?> GetByIdAsync(int id)
        {
            return await _abonnementDAO.GetByIdAsync(id);
        }
    }
}
