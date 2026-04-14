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
