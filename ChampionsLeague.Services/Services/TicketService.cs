using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;

//TicketService beheert tickets na order

namespace ChampionsLeague.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketDAO _ticketDAO;

        public TicketService(ITicketDAO ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }

        public async Task<Ticket?> GetByIdAsync(int ticketId)
        {
            return await _ticketDAO.GetByIdAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetByUserAsync(string userId)
        {
            return await _ticketDAO.GetByUserAsync(userId);
        }

        public async Task<int> CountTicketsByUserAndMatchAsync(string userId, int matchId)
        {
            return await _ticketDAO.CountTicketsByUserAndMatchAsync(userId, matchId);
        }

        public async Task<bool> HeeftTicketOpZelfdeDagAsync(string userId, DateOnly matchDate, int matchId)
        {
            return await _ticketDAO.HeeftTicketOpZelfdeDagAsync(userId, matchDate, matchId);
        }

        // Ticket status updaten naar annulatie
        //R-9: tot 1 week voor de start van de match
        //Zie ook unit test: ChampionsLeagueTests/TestTicketAnnulatie
        public async Task AnnuleerAsync(int ticketId)
        {
            var ticket = await _ticketDAO.GetByIdAsync(ticketId); 
            if (ticket == null) throw new Exception("Geen ticket gevonden.");

            if (ticket.Match?.MatchDate != null)
            {
                var deadline = ticket.Match.MatchDate.Value.AddDays(-7);
                if (DateOnly.FromDateTime(DateTime.Now) > deadline)
                    throw new Exception("Ticket kan niet meer gratis geannuleerd worden");
            }

            ticket.Status = "geannuleerd";

            await _ticketDAO.UpdateAsync(ticket);
            await _ticketDAO.SaveAsync();
        }

        // Geannuleerde tickets verwijderen
        public async Task DeleteGeannuleerdTicketAsync(int zitplaatsId, int matchId)
        {
            await _ticketDAO.DeleteGeannuleerdTicketAsync(zitplaatsId, matchId);
        }
    }
}
