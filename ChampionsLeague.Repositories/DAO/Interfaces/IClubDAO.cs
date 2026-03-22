using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IClubDAO
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club> GetByIdAsync(int id);
        Task<Club?> GetByNaamAsync(string naam);

    }
}
