using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Repositories.DAO

{
    public interface IMatchDAO
    {
        Task<List<Match>> GetAllMatchesAsync();
        Task<Match?> GetMatchByIdAsync(int id);
        Task<List<Match>> GetMatchesByClubAsync(int clubId);

       


    }
}