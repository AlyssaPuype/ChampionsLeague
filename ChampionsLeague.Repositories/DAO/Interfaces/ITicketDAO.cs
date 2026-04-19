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

        //get aantal ticket van een user van een match
        Task<int> CountTicketsByUserAndMatchAsync(string userId, int matchId);

        //Check of er al een ticket voor een andere match op dezelfde dag is gekocht voor deze gebruiker
        Task<bool> HeeftTicketOpZelfdeDagAsync(string userId, DateOnly matchDate, int matchId);

        //Status van ticket veranderen bij annulatie
        Task UpdateAsync(Ticket ticket);

        //Delete ticket na annulatie
        Task DeleteGeannuleerdTicketAsync(int zitplaatsId, int matchId);

        Task SaveAsync();
    }
}
