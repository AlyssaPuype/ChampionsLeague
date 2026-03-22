using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubDAO _clubDAO;

        public ClubService(IClubDAO clubDAO)
        {
            _clubDAO = clubDAO;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            return await _clubDAO.GetAllClubsAsync();
        }

        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _clubDAO.GetByIdAsync(id);
        }
    }
}