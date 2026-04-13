using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO
{
    public class OrderDAO : IOrderDAO
    {
        private readonly ChampionsLeagueDbContext _context;

    

    public OrderDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)        
                .Include(o => o.Orderlines) 
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Orderlines)
                .ThenInclude(ol => ol.Tickets)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<Order>> GetByUserAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Orderlines)
                .ThenInclude(ol => ol.Tickets)
                .ThenInclude(t => t.Match)
                .ToListAsync();

        }

        //order in database toevoegen
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
