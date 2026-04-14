using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IOrderDAO
    {
        
        Task AddAsync(Order order);
        Task SaveAsync();

    }
}
