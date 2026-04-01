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
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetByUserAsync(string userId)
        {
            //Include thuis en bezoekersclub en zitplaats
            return await _context.Tickets
                .Include(t => t.Match).ThenInclude(m => m.Thuisclub)
                .Include(t => t.Match).ThenInclude(m => m.Bezoekersclub)
                .Include(t => t.Match).ThenInclude(m => m.Stadion)
                .Include(t => t.Zitplaats).ThenInclude(z => z.Stadionvak)
                .Include(t => t.Zitplaats).ThenInclude(z => z.Stadionvak)
                .Include(t => t.Orderline).ThenInclude(ol => ol!.Order)
                .Where(t => t.Orderline!.Order.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        
        //methode voor validatie van max 4 tickets per user per match aan te kopen
        public async Task<int> CountTicketsByUserAndMatchAsync(string userId, int matchId)
        {
            return await _context.Tickets
                .Where(t => t.Orderline!.Order.UserId == userId)
                .Where(t => t.MatchId == matchId)
                .Where(t => t.Status != "geannuleerd")
                .CountAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}