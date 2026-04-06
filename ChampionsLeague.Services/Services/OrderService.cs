using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using ChampionsLeague.Util.Mail.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace ChampionsLeague.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAO _orderDAO;
        //zitplaatsservice om beschikbare zitplaatsen op te halen 
        private readonly IZitplaatsService _zitplaatsService;
        //constante ticketprijs voor nu
        private const decimal TicketPrijs = 50m;

        //ticketservice (max 4 tickets per user per match)
        private readonly ITicketService _ticketService;

        //mag geen tickets kopen voor twee verschillende matches op een dag
        private readonly IMatchService _matchService;

        private readonly IEmailSend _emailSend;

        public OrderService(IOrderDAO orderDAO, IZitplaatsService zitplaatsService, ITicketService ticketService, IMatchService matchService, IEmailSend emailSend)
        {
            _orderDAO = orderDAO;
            _zitplaatsService = zitplaatsService;
            _ticketService = ticketService;
            _matchService = matchService;
            _emailSend = emailSend;
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

        public async Task CreateTicketOrderAsync(string userId, string email, int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {

            //#18: Validatie: User mag max 4 tickets per match kopen
            var gekochteTickets = await _ticketService.CountTicketsByUserAndMatchAsync(userId, matchId);
            if (gekochteTickets + aantalGewensteZitplaatsen > 4)
                throw new Exception($"Reeds gekochte tickets: {gekochteTickets}. Maximum tickets per match: 4.");

            //#48: Validatie: User mag geen tickets kopen voor twee verschillende matches op dezelfde dag
            var match = await _matchService.GetMatchByIdAsync(matchId);
            if (match == null) throw new Exception("Match niet gevonden.");

            if (match.MatchDate != null)
            {
                var heeftTicketOpDag = await _ticketService.HeeftTicketOpZelfdeDagAsync(userId, match.MatchDate.Value, matchId);
                if (heeftTicketOpDag)
                    throw new Exception("Je hebt al een ticket voor een andere match op deze dag.");
            }

            //#50: Validatie: Een gebruiker mag enkel tickets kopen voor matches met date < 1 maand
            //
            if (match.MatchDate != null)
            {
                var maandlimiet = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
                if (match.MatchDate.Value > maandlimiet)
                    throw new Exception("Tickets kunnen pas 1 maand voor de wedstrijd gekocht worden.");
            }

            //TODO: #46 Error handling: stadionvak moet gekozen worden
            //get beschikbare zitplaatsen
            var beschikbareZitplaatsen = await _zitplaatsService.GetAvailableByStadionvakAsync(matchId, stadionvakId, aantalGewensteZitplaatsen);
            //Check of deze ingevuld worden
            if (!beschikbareZitplaatsen.Any())
            {
                throw new Exception("Geen beschikbare zitplaatsen.");
            }
            
            
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
                        Status = "gereserveerd",
                        Voucher = new Voucher
                        {
                            Code = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
                        }
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

            //Send voucher
            var voucherLines = orderline.Tickets
            .Select(t => $"<li>Zitplaats {t.ZitplaatsId} — Voucher: <strong>{t.Voucher.Code}</strong></li>")
            .ToList();

            await _emailSend.SendEmailAsync(email,
                "Bevestiging van uw tickets - ChampionsLeague",
                $@"<h2>Bedankt voor uw bestelling!</h2>
       <p>Uw vouchers:</p>
       <ul>{string.Join("", voucherLines)}</ul>
       <p>Totaalprijs: €{order.TotalePrijs}</p>");

        }
    

    }
}