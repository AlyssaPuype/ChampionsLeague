using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> GetAllMatchesAsync();
        Task<Match?> GetMatchByIdAsync(int id);
        Task<List<Match>> GetMatchesByClubAsync(int clubId);

        Task<Match?> GetEersteMatchVanClubAsync(int clubId);

    }
}