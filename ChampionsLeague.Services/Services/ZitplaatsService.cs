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

       
        public async Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {
            return await _zitplaatsDAO.GetAvailableByStadionvakAsync(matchId, stadionvakId, aantalGewensteZitplaatsen);
        }

        public async Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId)
        {
            return await _zitplaatsDAO.GetBeschikbareZitplaatsVoorAbonnementAsync(clubId);
        }
    }
}
