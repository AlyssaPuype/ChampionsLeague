using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club?> GetByIdAsync(int id);
        Task<Club?> GetByNaamAsync(string naam);
    }
}
