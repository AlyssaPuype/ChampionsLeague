using ChampionsLeague.Data.Interfaces;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace ChampionsLeague.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAO _orderDAO;
    }

    public OrderService(IOrderDAO orderDAO)
        {
            _orderDAO = orderDAO;

        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderDAO.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _orderDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByUserAsync(string userId)
        {
            return await _orderDAO.GetByUserAsync(userId);
        }


        public async Task CreateTicketOrderAsync(string userId, int matchId, int zitplaatsId)
        {
            var beschikbareZitplaatsen = 

            

        }

    }