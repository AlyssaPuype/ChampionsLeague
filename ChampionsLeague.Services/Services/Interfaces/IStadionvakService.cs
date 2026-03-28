using ChampionsLeague.Domains.Entities;

using System;
using System.Collections.Generic;
using System.Text;


namespace ChampionsLeague.Services.Services
{
    public interface IStadionvakService
    {
        Task<IEnumerable<Stadionvak>> GetAllAsync();
        Task<Stadionvak?> GetByIdAsync(int id);
        Task<IEnumerable<Stadionvak>> GetByStadionAsync(int stadionId);
    }
}