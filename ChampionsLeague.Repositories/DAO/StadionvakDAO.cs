using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO
{
    public class StadionvakDAO : IStadionvakDAO
    {
        private readonly ChampionsLeagueDbContext _context;
        public StadionvakDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Stadionvak?> GetByIdAsync(int id)
        {
            return await _context.Stadionvakken
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Stadionvak>> GetByStadionAsync(int stadionId)
        {
            return await _context.Stadionvakken
                .Where(s => s.StadionId == stadionId)
                .ToListAsync();
        }



    }
}
