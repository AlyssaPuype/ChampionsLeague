using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;

namespace ChampionsLeague.Services.Services
{
    public class CompetitieService : ICompetitieService
    {
        private readonly ICompetitieDAO _competitieDAO;

        public CompetitieService(ICompetitieDAO competitieDAO)
        {
            _competitieDAO = competitieDAO;
        }

        public async Task<DateOnly?> GetStartDatumAsync()
        {
            return await _competitieDAO.GetStartDatumAsync();
        }
    }
}
