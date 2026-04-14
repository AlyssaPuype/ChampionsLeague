using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;


namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface ITicketDAO
    {
        Task<Ticket?> GetByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetByUserAsync(string userId);
        Task UpdateAsync(Ticket ticket);

        //get aantal ticket van een user van een match
        Task<int> CountTicketsByUserAndMatchAsync(string userId, int matchId);

        //Check of er al een ticket voor een andere match op dezelfde dag is gekocht voor deze gebruiker
        Task<bool> HeeftTicketOpZelfdeDagAsync(string userId, DateOnly matchDate, int matchId);

        Task SaveAsync();
    }
}
