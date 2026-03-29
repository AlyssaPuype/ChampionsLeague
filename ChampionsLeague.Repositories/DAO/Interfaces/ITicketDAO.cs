using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface ITicketDAO
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetByUserAsync(string userId);
        Task UpdateAsync(Ticket ticket);
        Task SaveAsync();
    }
}
