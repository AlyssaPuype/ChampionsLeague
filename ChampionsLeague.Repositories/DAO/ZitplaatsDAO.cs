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

        //Get alle beschikbare zitplaatsen
        public async Task<IEnumerable<Zitplaats>> GetBeschikbaarPerStadionvakAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {
            return await _context.Zitplaatsen
                .Where(z => z.StadionvakId == stadionvakId)
                .Where(z => !z.Tickets.Any(t => t.MatchId == matchId && t.Status != "geannuleerd")) // ← enkel deze
                .Where(z => !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id))
                .Take(aantalGewensteZitplaatsen)
                .ToListAsync();
        }

        //Get 1 beschikbare zitplaats voor abonnement
        public async Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int clubId)
        {
            var stadionId = await _context.Clubs
                .Where(c => c.Id == clubId)
                .Select(c => c.StadionId)
                .FirstOrDefaultAsync();

            return await _context.Zitplaatsen
                .Where(z => z.Stadionvak.StadionId == stadionId
                    && !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id)
                    && !_context.Tickets.Any(t => t.ZitplaatsId == z.Id && t.Status != "geannuleerd"))
                .FirstOrDefaultAsync();
        }

        //Get aantal beschikbare zitplaatsen voor een stadionvak
        public async Task<int> GetAantalBeschikbaarAsync(int stadionvakId, int matchId)
        {
            return await _context.Zitplaatsen
                .Where(z => z.StadionvakId == stadionvakId)
                .Where(z => !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id))
                .Where(z => matchId == 0 || !z.Tickets.Any(t => t.MatchId == matchId && t.Status != "geannuleerd"))
                .CountAsync();
        }
    }
}
