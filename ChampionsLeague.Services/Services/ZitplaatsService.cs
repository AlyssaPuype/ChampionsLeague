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

       
        public async Task<IEnumerable<Zitplaats>> GetBeschikbaarPerStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {
            return await _zitplaatsDAO.GetBeschikbaarPerStadionvakAsync(matchId, stadionvakId, aantalGewensteZitplaatsen);
        }

        public async Task<int> GetAantalBeschikbaarAsync(int stadionvakId, int matchId)
        {
            return await _zitplaatsDAO.GetAantalBeschikbaarAsync(stadionvakId, matchId);
        }

        public async Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId)
        {
            return await _zitplaatsDAO.GetBeschikbareZitplaatsVoorAbonnementAsync(clubId);
        }
    }
}
