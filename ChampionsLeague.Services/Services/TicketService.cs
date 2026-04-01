using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;

namespace ChampionsLeague.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketDAO _ticketDAO;

        public TicketService(ITicketDAO ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _ticketDAO.GetAllAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int ticketId)
        {
            return await _ticketDAO.GetByIdAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetByUserAsync(string userId)
        {
            return await _ticketDAO.GetByUserAsync(userId);
        }

        // we moeten een ticket kunnen annuleren
        public async Task UpdateAsync(Ticket ticket)
        {
            await _ticketDAO.UpdateAsync(ticket);
            await _ticketDAO.SaveAsync();
        }
    }
}
