using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ChampionsLeague.Repositories.DAO
{
    public class TicketDAO : ITicketDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public TicketDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        
        public async Task<Ticket?> GetByIdAsync(int ticketId)
        {
            return await _context.Tickets
                .Include(t => t.Match)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
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

               
        
        //methode voor validatie van max 4 tickets per user per match aan te kopen, rekening houden met geannuleerde tickets
        public async Task<int> CountTicketsByUserAndMatchAsync(string userId, int matchId)
        {
            return await _context.Tickets
                .Where(t => t.Orderline!.Order.UserId == userId)
                .Where(t => t.MatchId == matchId)
                .Where(t => t.Status != "geannuleerd")
                .CountAsync();
        }

        //methode voor validatie dat user geen tickets kan kopen voor twee =/ matches op dezelfde dag, rekening houden met geannuleerde tickets
        public async Task<bool> HeeftTicketOpZelfdeDagAsync(string userId, DateOnly matchDate, int matchId)
        {
            return await _context.Tickets
                .Include(t => t.Match)
                .Where(t => t.Orderline!.Order.UserId == userId)
                .Where(t => t.Status != "geannuleerd")
                .Where(t => t.Match!.MatchDate == matchDate)
                .Where(t => t.MatchId != matchId) //dezelfde match uitsluiten, user mag wel nog verder bestellen voor dezelfde match
                .AnyAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        //Delete ticket en voucher van de DB
        public async Task DeleteGeannuleerdTicketAsync(int zitplaatsId, int matchId)
        {
            var ticket = await _context.Tickets
            .Include(t => t.Voucher)
            .FirstOrDefaultAsync(t => t.ZitplaatsId == zitplaatsId
            && t.MatchId == matchId
            && t.Status == "geannuleerd");

            if (ticket != null)
            {
                if (ticket.Voucher != null)
                    _context.Vouchers.Remove(ticket.Voucher);

                _context.Tickets.Remove(ticket);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}