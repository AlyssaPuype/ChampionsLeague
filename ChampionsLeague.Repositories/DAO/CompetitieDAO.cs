using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO
{
    public class CompetitieDAO : ICompetitieDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public CompetitieDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<DateOnly?> GetStartDatumAsync()
        {
            var competitie = await _context.Competities.FirstOrDefaultAsync();
            return competitie?.StartDatum;
        }
    }
}
