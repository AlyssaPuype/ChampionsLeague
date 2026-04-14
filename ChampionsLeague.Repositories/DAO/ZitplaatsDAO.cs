using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Repositories.DAO
{
    public class ZitplaatsDAO : IZitplaatsDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public ZitplaatsDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Zitplaats>> GetAvailableByStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {
            return await _context.Zitplaatsen
                .Where(z => z.StadionvakId == stadionvakId)
                .Where(z => !z.Tickets.Any(t => t.MatchId == matchId))
                .Where(z => !z.Tickets.Any(t => t.MatchId == matchId && t.Status != "geannuleerd"))
                .Where(z => !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id))
                .Take(aantalGewensteZitplaatsen)
                .ToListAsync();
        }
    }
}
