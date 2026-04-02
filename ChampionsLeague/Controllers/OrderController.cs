using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Extensions;
using ChampionsLeague.Models;
using ChampionsLeague.Models.Order;
using ChampionsLeague.Services;
using ChampionsLeague.Services.Services;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMatchService _matchService;
        private readonly IStadionvakService _stadionvakService;
        private readonly ITicketService _ticketService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IMatchService matchService, IStadionvakService stadionvakService, ITicketService ticketService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _matchService = matchService;
            _stadionvakService = stadionvakService;
            _ticketService = ticketService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int matchId)
        {
            var match = await _matchService.GetMatchByIdAsync(matchId);

            var vakken = await _stadionvakService.GetByStadionAsync(match.Stadion.Id);

            var viewModel = new OrderTicketVM
            {
                Match = match,
                Stadionvakken = vakken,
                GeselecteerdMatchId = matchId
            };

            return View(viewModel);
        }

        //De flow:
        //Na het selecteren van een match, komt de gebruiker op de Order/index terecht
        //De gebruiker selecteert een stadionvak
        //De gebruiker selecteert het aantal gewenste tickets
        //De gebruikter klikt op 'Toevoegen aan winkelmand'
        //AddToCart actie wordt getriggered, er is validatie op het stadionvak en business rules
        //Gebruiker kan naar de winkelmand en daar de bestelling afronden, dan wordt Payment() getriggered
        //Payment() calls CreateTicketOrderAsync() in OrderService, de tickets worden in de DB aangemaakt


        [HttpPost]
        public async Task<IActionResult> AddToCart(OrderTicketVM viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var match = await _matchService.GetMatchByIdAsync(viewModel.GeselecteerdMatchId);

            try
            {
                //Get current shoppingcart list from Session
                var currentCartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

                // validatie — stadionvak moet geselecteerd zijn
                if (viewModel.GeselecteerdStadionvakId == 0)
                    throw new Exception("Selecteer een stadionvak.");

                //#18: Validatie: User mag max 4 tickets per match (over alle stadionvakken) kopen
                var gekochteTickets = await _ticketService.CountTicketsByUserAndMatchAsync(user!.Id, viewModel.GeselecteerdMatchId);
                var ticketsInCart = currentCartList?.Carts?
                    .Where(c => c.MatchId == viewModel.GeselecteerdMatchId)
                    .Sum(c => c.AantalTickets) ?? 0;

                if (gekochteTickets + ticketsInCart + viewModel.AantalTickets > 4)
                    throw new Exception($"Maximum 4 tickets per match. Al gekocht: {gekochteTickets}, in winkelmand: {ticketsInCart}.");


                //#48: Validatie: User mag geen tickets kopen voor twee verschillende matches op dezelfde dag
                if (match == null) throw new Exception("Match niet gevonden.");

                if (match.MatchDate != null)
                {
                    //Dit checkt of er al een ticket voor een match bestaat in de database
                    var heeftTicketOpDag = await _ticketService.HeeftTicketOpZelfdeDagAsync(user!.Id, match.MatchDate.Value, viewModel.GeselecteerdMatchId);
                    if (heeftTicketOpDag)
                        throw new Exception("Je hebt al een ticket voor een andere match op deze dag.");

                    //dit checkt of er al een ticket voor een andere match in de shopping cart zit.
                    if (currentCartList?.Carts != null && currentCartList.Carts.Any())
                    {
                        var heeftMatchInCartOpDag = currentCartList.Carts.Any(c =>
                            c.MatchId != viewModel.GeselecteerdMatchId &&
                            c.MatchDatum == match.MatchDate.Value.ToString("dd/MM/yyyy"));
                        if (heeftMatchInCartOpDag)
                            throw new Exception("Je hebt al een ticket voor een andere match op deze dag in je winkelmand.");
                    }
                }

                //#50: Validatie: Een gebruiker mag enkel tickets kopen voor matches met date < 1 maand
                //
                if (match.MatchDate != null)
                {
                    var maandlimiet = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
                    if (match.MatchDate.Value > maandlimiet)
                        throw new Exception("Tickets kunnen pas 1 maand voor de wedstrijd gekocht worden.");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                viewModel.Match = match!;
                viewModel.Stadionvakken = await _stadionvakService.GetByStadionAsync(match!.Stadion.Id);
                return View("Index", viewModel);
            }

            // Validaties ok -> toevoegen aan shoppingcart
            var vak = await _stadionvakService.GetByIdAsync(viewModel.GeselecteerdStadionvakId);
            var cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart")
                ?? new ShoppingCartVM { Carts = new List<CartItemVM>() };

            // check if match already in cart
            var bestaandItem = cartList.Carts!.FirstOrDefault(c => c.MatchId == viewModel.GeselecteerdMatchId && c.StadionvakId == viewModel.GeselecteerdStadionvakId);
            if (bestaandItem != null)
            {
                bestaandItem.AantalTickets += viewModel.AantalTickets;
            }
            else
            {
                cartList.Carts!.Add(new CartItemVM
                {
                    MatchId = match!.Id,
                    MatchNaam = $"{match.Thuisclub?.Naam} vs {match.Bezoekersclub?.Naam}",
                    MatchDatum = match.MatchDate?.ToString("dd/MM/yyyy"),
                    StadionNaam = match.Stadion?.Naam,
                    StadionvakId = vak!.Id,
                    StadionvakNaam = vak.Naam,
                    AantalTickets = viewModel.AantalTickets,
                    Prijs = 50m
                });
            }

            HttpContext.Session.SetObject("ShoppingCart", cartList);

            return RedirectToAction("Index", "ShoppingCart");
        }

        //niet meer gebruiken
        [HttpPost]
        public async Task<IActionResult> CreateTicket(OrderTicketVM viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            //null check op user:
            if (user == null) return Unauthorized();

            try
            {
                //Probleem: #39 : Ticket object werd niet aangemaakt in OrderService
                //Breakpoint hier gezet, kijk welke waarde matchId, StadionvakId en AantalTickets heeft. 
                //viewModel.GeslecteerdMatchId: 0
                //viewModel.GeselecteerdStadionvakId: 0
                //viewModel.AantalTickets: 1
                //Oplossing:
                    //in Views/Order/Index.cshtml input field voor GeselecteerdMatchId ontbreekt, toevoegen
                    //In OrderService.cs: order van attributen was omgedraaid, opgelost door matchId als eerste te zetten. var beschikbareZitplaatsen = await _zitplaatsService.GetAvailableByStadionvakAsync(matchId, stadionvakId, aantalGewensteZitplaatsen);
                //Test: stadionvak: Real Madrid - Mancity 26/04/01. Onder-West, Aantal tickets: 2 -> output: MatchId: 31, StadionvakId: 248, AantalTickets: 2
                await _orderService.CreateTicketOrderAsync(user.Id, viewModel.GeselecteerdMatchId, viewModel.GeselecteerdStadionvakId, viewModel.AantalTickets);

                return RedirectToAction("Index", "Home");
            } 
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                var match = await _matchService.GetMatchByIdAsync(viewModel.GeselecteerdMatchId);
                var vakken = await _stadionvakService.GetByStadionAsync(match!.Stadion.Id);
                viewModel.Match = match;
                viewModel.Stadionvakken = vakken;

                return View("Index", viewModel);
            }
            

        }
    }
}
