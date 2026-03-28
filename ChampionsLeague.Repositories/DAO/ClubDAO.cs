using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ChampionsLeague.Web.DAO
{
    public class ClubDAO : IClubDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public ClubDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            return await _context.Clubs
                .Include(i => i.Stadion)
                .ToListAsync();
        }

        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs
                .Include(i => i.Stadion)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Club?> GetByNaamAsync(string naam)
        {
            return await _context.Clubs
                .Include(i => i.Stadion)
                .FirstOrDefaultAsync(i => i.Naam == naam);
        }
    }
}