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

        public async Task<IEnumerable<Abonnement>> GetAllAsync()
        {
            return await _context.Abonnements
                .Include(a => a.Club)
                .Include(a => a.Zitplaats)
                .ToListAsync();
        }

        public async Task<Abonnement?> GetByIdAsync(int id)
        {
            return await _context.Abonnements
                .Include(a => a.Club)
                .Include(a => a.Zitplaats)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId)
        {
            return await _context.Abonnements
                .AnyAsync(a => a.ClubId == clubId
                    && a.Orderline != null
                    && a.Orderline.Order.UserId == userId);
        }

        // get a zitplaats in the club's stadion that isn't already taken by an abonnement
        public async Task<Zitplaats?> GetBeschikbareZitplaatsAsync(int clubId)
        {
            var stadionId = await _context.Clubs
                .Where(c => c.Id == clubId)
                .Select(c => c.StadionId)
                .FirstOrDefaultAsync();

            return await _context.Zitplaatsen
                .Where(z => z.Stadionvak.StadionId == stadionId
                    && !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id))
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Abonnement abonnement)
        {
            await _context.Abonnements.AddAsync(abonnement);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
