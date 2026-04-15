using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO;
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

        //Zitplaatsservice  (beschikbare zitplaatsen op halen voor tickets en abonnementen)
        private readonly IZitplaatsService _zitplaatsService;

        //INJECTION voor tickets:
        //Ticketservice (max 4 tickets per user per match)
        //Matchservice (mag geen tickets kopen voor twee verschillende matches op een dag)
        //constante ticketprijs op 50 euro

        private readonly ITicketService _ticketService;
        private readonly IMatchService _matchService;
        private const decimal TicketPrijs = 50m;


        //Voordien had ik logica om abonnementen aan te maken in abonnementservice, logica naar orderservice verplaatst
        
        //INJECTION voor Abonnementen:
        //AbonnementDAO (check of gebruiker al een abonnement heeft voor die club)
        //ClubService (vind club voor abonnement)
        //CompetitieService (mag geen abonnement kopen na de start van de competitie)
        //constante abonnementprijs op 200 euro
        private readonly IAbonnementDAO _abonnementDAO;
        private readonly IClubService _clubService;
        private readonly ICompetitieService _competitieService;
        private const decimal AbonnementPrijs = 200m;
        
        //INJECTION om emails te kunnen sturen (voor vouchers)
        private readonly IEmailSend _emailSend;

        public OrderService(IOrderDAO orderDAO, IZitplaatsService zitplaatsService, ITicketService ticketService, IMatchService matchService, IAbonnementDAO abonnementDAO, IClubService clubService, ICompetitieService competitieService, IEmailSend emailSend)
        {
            _orderDAO = orderDAO;
            _zitplaatsService = zitplaatsService;
            _ticketService = ticketService;
            _matchService = matchService;
            _abonnementDAO = abonnementDAO;
            _clubService = clubService;
            _competitieService = competitieService;
            _emailSend = emailSend;
        }

        //Ticket aanmaken
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
            var beschikbareZitplaatsen = await _zitplaatsService.GetBeschikbaarPerStadionvakAsync(matchId, stadionvakId, aantalGewensteZitplaatsen);
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


        // abonnement aanmaken
        public async Task CreateAbonnementOrderAsync(string userId, string email, int clubId)
        {

            var club = await _clubService.GetByIdAsync(clubId);
            if (club == null) throw new Exception("Club niet gevonden.");

            //Validation: abonnement enkel voor start competitie kopen
            //Via competitieService
            //Zie ook unit test: ChampionsLeagueTests/TestAbonnement
            var startDatum = await _competitieService.GetStartDatumAsync();
            if (startDatum != null && DateOnly.FromDateTime(DateTime.Now) >= startDatum.Value)
                throw new Exception($"Abonnementen kunnen enkel gekocht worden vóór {startDatum.Value}.");


            //Validation: user heeft al abonnement voor deze club
            if (await _abonnementDAO.HeeftAbonnementVoorClubAsync(userId, clubId))
                throw new Exception("Je hebt al een abonnement voor deze club.");

            // zoek beschikbare zitplaats
            var zitplaats = await _zitplaatsService.GetBeschikbareZitplaatsVoorAbonnementAsync(clubId);
            if (zitplaats == null)
                throw new Exception("Geen beschikbare zitplaatsen meer voor dit abonnement.");

            // voeg abonnement toe aan orderline
            var orderline = new Orderline { Prijs = AbonnementPrijs };
            orderline.Abonnements.Add(new Abonnement
            {
                ClubId = clubId,
                ZitplaatsId = zitplaats.Id,
                Prijs = AbonnementPrijs
            });

            //maak order
            var order = new Order
            {
                UserId = userId,
                Orderlines = new List<Orderline> { orderline },
                TotalePrijs = AbonnementPrijs,
                OrderDate = DateTime.Now
            };

            await _orderDAO.AddAsync(order);
            await _orderDAO.SaveAsync();

            // send voucher
            await _emailSend.SendEmailAsync(
                email,
                "Bevestiging van uw abonnement - ChampionsLeague",
                $@"<h2>Bedankt voor uw abonnement!</h2>
               <p>U heeft een abonnement gekocht voor alle thuismatches van <strong>{club.Naam}</strong> .</p>
               <p>Zitplaats: <strong>{zitplaats.ZitplaatsNummer}</strong></p>
               <p>Prijs: €{AbonnementPrijs}</p>"
            );
        }



    }
}