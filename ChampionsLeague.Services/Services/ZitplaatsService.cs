using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services
{
    public class ZitplaatsService : IZitplaatsService
    {
        private readonly IZitplaatsDAO _zitplaatsDAO;

        public ZitplaatsService(IZitplaatsDAO zitplaatsDAO)
        {
            _zitplaatsDAO = zitplaatsDAO;
        }

        public async Task<IEnumerable<Zitplaats>> GetAllAsync()
        {
            return await _zitplaatsDAO.GetAllAsync();
        }

        public async Task<Zitplaats?> GetByIdAsync(int id)
        {
            return await _zitplaatsDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int stadionvakId, int matchId, int aantalGewensteZitplaatsen)
        {
            return await _zitplaatsDAO.GetAvailableByStadionvakAsync(stadionvakId, matchId, aantalGewensteZitplaatsen);
        }
    }
}
