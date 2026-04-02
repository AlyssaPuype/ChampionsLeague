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
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IMatchService matchService, IStadionvakService stadionvakService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _matchService = matchService;
            _stadionvakService = stadionvakService;
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(OrderTicketVM viewModel)
        {
            // validatie — stadionvak moet geselecteerd zijn
            if (viewModel.GeselecteerdStadionvakId == 0)
            {
                ModelState.AddModelError("", "Selecteer een stadionvak.");

                var matchOpnieuw = await _matchService.GetMatchByIdAsync(viewModel.GeselecteerdMatchId);
                var vakkenOpnieuw = await _stadionvakService.GetByStadionAsync(matchOpnieuw!.Stadion.Id);
                viewModel.Match = matchOpnieuw;
                viewModel.Stadionvakken = vakkenOpnieuw;

                return View("Index", viewModel);
            }

            var match = await _matchService.GetMatchByIdAsync(viewModel.GeselecteerdMatchId);
            var vak = await _stadionvakService.GetByIdAsync(viewModel.GeselecteerdStadionvakId);

            // get cart or create new
            var cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart")
                ?? new ShoppingCartVM { Carts = new List<CartItemVM>() };

            // check if match already in cart
            var bestaandItem = cartList.Carts!.FirstOrDefault(c => c.MatchId == viewModel.GeselecteerdMatchId);
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
