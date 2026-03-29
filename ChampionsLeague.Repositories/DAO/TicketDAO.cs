using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO
{
    public class TicketDAO : ITicketDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public TicketDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int ticketId)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId); // ← filter on id
        }

        public async Task<IEnumerable<Ticket>> GetByUserAsync(string userId)
        {
            return await _context.Tickets.Where(t => t.Orderline!.Order.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}