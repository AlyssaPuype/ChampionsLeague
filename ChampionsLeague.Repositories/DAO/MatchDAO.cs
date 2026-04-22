using ChampionsLeague.Data.Interfaces;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Repositories.DAO

{
    public class MatchDAO : IMatchDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public MatchDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            return await _context.Matches
                .Include(m => m.Thuisclub)
                .Include(m => m.Bezoekersclub)
                .Include(m => m.Stadion)
                .Include(m => m.Competitie)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();
        }

        public async Task<Match?> GetMatchByIdAsync(int id)
        {
            return await _context.Matches
                .Include(m => m.Thuisclub)
                .Include(m => m.Bezoekersclub)
                .Include(m => m.Stadion)
                .Include(m => m.Competitie)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Match>> GetMatchesByClubAsync(int clubId)
        {
            return await _context.Matches
                .Include(m => m.Thuisclub)
                .Include(m => m.Bezoekersclub)
                .Include(m => m.Stadion)
                .Include(m => m.Competitie)
                .Where(m => m.Thuisclub.Id == clubId || m.Bezoekersclub.Id == clubId)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();
        }

       
    }
}