using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface IOrderDAO
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserAsync(string userId);
        Task AddAsync(Order order);

    }
}
