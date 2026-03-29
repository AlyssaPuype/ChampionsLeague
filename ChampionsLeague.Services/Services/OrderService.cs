using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace ChampionsLeague.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAO _orderDAO;
        //zitplaatsservice om beschikbare zitplaatsen op te halen 
        private readonly IZitplaatsService _zitplaatsService;
        //constante ticketprijs voor nu
        private const decimal TicketPrijs = 50m;

        public OrderService(IOrderDAO orderDAO, IZitplaatsService zitplaatsService)
        {
            _orderDAO = orderDAO;
            _zitplaatsService = zitplaatsService;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderDAO.GetAllOrdersAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _orderDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByUserAsync(string userId)
        {
            return await _orderDAO.GetByUserAsync(userId);
        }


        public async Task CreateTicketOrderAsync(string userId, int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {
            //get beschikbare zitplaatsen
            var beschikbareZitplaatsen = await _zitplaatsService.GetAvailableByStadionvakAsync(matchId, stadionvakId, aantalGewensteZitplaatsen);

            //maak orderline aan en bereken totaalprijs
            var orderline = new Orderline
            {
                Prijs = TicketPrijs * aantalGewensteZitplaatsen
            };


            //voeg ticket toe aan orderline
            foreach (var zitplaats in beschikbareZitplaatsen)
            {
                orderline.Tickets
                    .Add(new Ticket
                    {
                        ZitplaatsId = zitplaats.Id,
                        MatchId = matchId,
                        Prijs = TicketPrijs,
                        Status = "Gereserveerd"
                    });
            }

            //maak order
            var order = new Order
            {
                UserId = userId,
                Orderlines = new List<Orderline>{orderline},
                TotalePrijs = orderline.Prijs,
                OrderDate = DateTime.Now

            };


            await _orderDAO.AddAsync(order);
            await _orderDAO.SaveAsync();

        }
    

    }
}