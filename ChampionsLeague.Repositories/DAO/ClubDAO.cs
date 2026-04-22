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
                .Include(c => c.Stadion)
                .ToListAsync();
        }

        // get clubs by id
        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs
                .Include(c => c.Stadion)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // get clubs by naam
        public async Task<Club?> GetByNaamAsync(string naam)
        {
            return await _context.Clubs
                .Include(c => c.Stadion)
                .FirstOrDefaultAsync(c => c.Naam == naam);
        }
    }
}