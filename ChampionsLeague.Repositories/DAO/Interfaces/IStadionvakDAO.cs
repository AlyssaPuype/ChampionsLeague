using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IStadionvakDAO
    {
        Task<IEnumerable<Stadionvak>> GetAllStadionvakkenAsync();
        Task<Stadionvak?> GetByIdAsync(int id);
        Task<IEnumerable<Stadionvak>> GetByStadionAsync(int stadionId);
    }
}
