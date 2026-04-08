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
