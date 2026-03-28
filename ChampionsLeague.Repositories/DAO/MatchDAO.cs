using ChampionsLeague.Data.Interfaces;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Data.DAOs
{
    public class MatchDAO : IMatchDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public MatchDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetAllAsync()
        {
            return await _context.Matches
                .Include(i => i.Thuisclub)
                .Include(i => i.Bezoekersclub)
                .Include(i => i.Stadion)
                .Include(i => i.Competitie)
                .OrderBy(i => i.MatchDate)
                .ToListAsync();
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            return await _context.Matches
                .Include(i => i.Thuisclub)
                .Include(i => i.Bezoekersclub)
                .Include(i => i.Stadion)
                .Include(i => i.Competitie)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Match>> GetByClubAsync(int clubId)
        {
            return await _context.Matches
                .Include(i => i.Thuisclub)
                .Include(i => i.Bezoekersclub)
                .Include(i => i.Stadion)
                .Include(i => i.Competitie)
                .Where(i => i.Thuisclub.Id == clubId || m.Bezoekersclub.Id == clubId)
                .OrderBy(i => i.MatchDate)
                .ToListAsync();
        }

        
    }
}