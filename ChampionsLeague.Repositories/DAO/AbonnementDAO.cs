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
