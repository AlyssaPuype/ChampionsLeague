using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO
{
    public class AbonnementDAO : IAbonnementDAO
    {

        private readonly ChampionsLeagueDbContext _context;

        public AbonnementDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId)
        {
            return await _context.Abonnements
                .AnyAsync(a => a.ClubId == clubId
                    && a.Orderline != null
                    && a.Orderline.Order.UserId == userId);
        }

        // Zitplaats zoeken die geen ticket of abonnement al heeft.
        public async Task<Zitplaats?> GetBeschikbareZitplaatsAsync(int clubId)
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

        // get alle abonnementen van een gebruiker met club info, zitplaats, stadionvak
        public async Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId)
        {
            return await _context.Abonnements
                .Include(a => a.Club)
                    .ThenInclude(c => c.Stadion)
                .Include(a => a.Zitplaats)
                    .ThenInclude(z => z.Stadionvak)
                .Include(a => a.Orderline)
                    .ThenInclude(ol => ol.Order)
                .Where(a => a.Orderline != null && a.Orderline.Order.UserId == userId)
                .ToListAsync();
        }


    }
}
