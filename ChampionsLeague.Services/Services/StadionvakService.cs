using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public class StadionvakService : IStadionvakService
    {
        private readonly IStadionvakDAO _stadionvakDAO;

        public StadionvakService(IStadionvakDAO stadionvakDAO)
        {
            _stadionvakDAO = stadionvakDAO;
        }
        public async Task<IEnumerable<Stadionvak>> GetAllStadionvakkenAsync()
        {
            return await _stadionvakDAO.GetAllStadionvakkenAsync();
        }

        public async Task<Stadionvak?> GetByIdAsync(int id)
        {
            return await _stadionvakDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Stadionvak>> GetByStadionAsync(int stadionId)
        {
            return await _stadionvakDAO.GetByStadionAsync(stadionId);
        }
    }
}

