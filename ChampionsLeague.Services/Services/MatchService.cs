using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Data.Interfaces;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Services.Services.Interfaces;

namespace ChampionsLeague.Services.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchDAO _matchDAO;

        public MatchService(IMatchDAO matchDAO)
        {
            _matchDAO = matchDAO;
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            return await _matchDAO.GetAllMatchesAsync();
        }

        public async Task<Match?> GetMatchByIdAsync(int id)
        {
            return await _matchDAO.GetByIdAsync(id);
        }

        public async Task<List<Match>> GetMatchesByClubAsync(int clubId)
        {
            return await _matchDAO.GetByClubAsync(clubId);
        }

    }
}