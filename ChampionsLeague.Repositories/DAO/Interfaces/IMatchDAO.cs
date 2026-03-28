using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Data.Interfaces
{
    public interface IMatchDAO
    {
        Task<List<Match>> GetAllAsync();
        Task<Match?> GetByIdAsync(int id);
        Task<List<Match>> GetByClubAsync(int clubId);

    }
}