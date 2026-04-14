using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket?> GetByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetByUserAsync(string userId);
        Task<int> CountTicketsByUserAndMatchAsync(string userId, int matchId);
        Task<bool> HeeftTicketOpZelfdeDagAsync(string userId, DateOnly matchDate, int matchId);

        Task UpdateAsync(Ticket ticket);

    }
}
