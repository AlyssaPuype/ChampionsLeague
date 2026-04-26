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


//Ordercontroller en orderservice beheren beide tickets en abonnement orders
//uitrbreiden later om ook hotelboekingen te beheren?

namespace ChampionsLeague.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMatchService _matchService;
        private readonly IStadionvakService _stadionvakService;
        private readonly ITicketService _ticketService;
        private readonly IClubService _clubService;
        private readonly IZitplaatsService _zitplaatsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IMatchService matchService, IStadionvakService stadionvakService, ITicketService ticketService, IClubService clubService, IZitplaatsService zitplaatsService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _matchService = matchService;
            _stadionvakService = stadionvakService;
            _ticketService = ticketService;
            _clubService = clubService;
            _zitplaatsService = zitplaatsService;
            _userManager = userManager;
        }

        //Beschikbare zitplaatsen zien, returns json zodat ajax deze data kan gebruiken (gebruikt in orderindex van ticket om beschikbare zitplaatsen weer te geven)
        [HttpGet]
        public async Task<IActionResult> GetAantalBeschikbaar(int stadionvakId, int matchId = 0)
        {
            var aantal = await _zitplaatsService.GetAantalBeschikbaarAsync(stadionvakId, matchId);
            
            return Json(aantal);
        }

        //ORDERPROCES: TICKETS
        [HttpGet]
        public async Task<IActionResult> Ticket(int matchId)
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
        //Na het selecteren van een match, komt de gebruiker op de Order/Ticket pagina terecht
        //De gebruiker selecteert een stadionvak en het aantal gewenste tickets
        //De gebruiker klikt op 'Toevoegen aan winkelmand'
        //AddTicketToCart actie wordt getriggerd + validatie op business rules
        //Gebruiker gaat naar de winkelmand en rondt de bestelling af via CreateOrder() in ShoppingCartController
        //CreateOrder() roept CreateTicketOrderAsync() aan in OrderService
        //Tickets worden aangemaakt in de DB en een bevestigingsmail wordt verstuurd


        //TODO: capacity houdt nog geen rekening met zitplaatsen of abonnementen in shoppingcart, het update alleen nadat de order is geplaatst
        //Ticketorder valideren en toevoegen aan shoppingcart (alleen controller heeft toegang tot de session, daarom gebeurt de validaties op tickets en abonnement nogmaals hier in de controller
        [HttpPost]
        public async Task<IActionResult> AddTicketToCart(OrderTicketVM viewModel)
        {
            //gebruiker en match ophalen
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var match = await _matchService.GetMatchByIdAsync(viewModel.GeselecteerdMatchId);

            //Cart ophalen
            var cart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart")
            ?? new ShoppingCartVM { Carts = new List<CartItemVM>() };
            cart.Carts ??= new List<CartItemVM>();

            //clear model binding validations (eigen validatie error)
            ModelState.Clear();
            try
            {

                // validatie — stadionvak moet geselecteerd zijn
                if (viewModel.GeselecteerdStadionvakId == 0)
                    throw new Exception("Selecteer een stadionvak.");

                //#18: Validatie: User mag max 4 tickets per match (over alle stadionvakken) kopen
   
                var gekochteTickets = await _ticketService.CountTicketsByUserAndMatchAsync(user.Id, viewModel.GeselecteerdMatchId);
                var ticketsInCart = cart.Carts
               .Where(c => c.MatchId == viewModel.GeselecteerdMatchId)
               .Sum(c => c.AantalTickets);
                //LINQ in controller gebruikt -> session zit in weblaag

                if (gekochteTickets + ticketsInCart + viewModel.AantalTickets > 4)
                    throw new Exception($"Maximum 4 tickets per match. Al gekocht: {gekochteTickets}, in winkelmand: {ticketsInCart}.");


                //#48: Validatie: User mag geen tickets kopen voor twee verschillende matches op dezelfde dag
                if (match == null) throw new Exception("Match niet gevonden.");

                if (match.MatchDate != null)
                {
                   
                    //Dit checkt of er al een ticket voor een match bestaat in de database
                    var heeftTicketOpDag = await _ticketService.HeeftTicketOpZelfdeDagAsync(user.Id, match.MatchDate.Value, viewModel.GeselecteerdMatchId);
                    if (heeftTicketOpDag)
                        throw new Exception("Je hebt al een ticket voor een andere match op deze dag.");

                    //dit checkt of er al een ticket voor een andere match in de shopping cart zit.
                    if (cart?.Carts != null && cart.Carts.Any())
                    {
                        var heeftMatchInCartOpDag = cart.Carts.Any(c =>
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
                return View("Ticket", viewModel);
            }

            // Validaties ok -> toevoegen aan shoppingcart
            var vak = await _stadionvakService.GetByIdAsync(viewModel.GeselecteerdStadionvakId);
          
            // check if match already in cart
            var bestaandItem = cart.Carts.FirstOrDefault(c => c.MatchId == viewModel.GeselecteerdMatchId && c.StadionvakId == viewModel.GeselecteerdStadionvakId);
            if (bestaandItem != null)
            {
                bestaandItem.AantalTickets += viewModel.AantalTickets;
            }
            else
            {
                cart.Carts!.Add(new CartItemVM
                {
                    MatchId = match!.Id,
                    ThuisclubId = match.ThuisclubId,
                    MatchNaam = $"{match.Thuisclub?.Naam} vs {match.Bezoekersclub?.Naam}",
                    MatchDatum = match.MatchDate?.ToString("dd/MM/yyyy"),
                    StadionNaam = match.Stadion?.Naam,
                    StadionvakId = vak!.Id,
                    StadionvakNaam = vak.Naam,
                    AantalTickets = viewModel.AantalTickets,
                    Prijs = 50m
                });
            }

            HttpContext.Session.SetObject("ShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }


        //ORDERPROCES: ABONNEMENTEN
        public async Task<IActionResult> Abonnement(int clubId)
        {
            var club = await _clubService.GetByIdAsync(clubId);
            if (club == null) return NotFound();

            var vakken = await _stadionvakService.GetByStadionAsync(club.StadionId);

            var viewModel = new OrderAbonnementVM
            {
                Club = club,
                Stadionvakken = vakken,
                ClubId = clubId
            };

            return View(viewModel);
        }

        //Abonnementorder valideren en toevoegen aan shoppingcart, 

        [HttpPost]
        public async Task<IActionResult> AddAbonnementToCart(OrderAbonnementVM viewModel)
        {
            //Club ophalen
            var club = await _clubService.GetByIdAsync(viewModel.ClubId);
            if (club == null) return NotFound();

            //Cart ophalen
            var cart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart")
                ?? new ShoppingCartVM();

            cart.AbonnementCarts ??= new List<AbonnementCartItemVM>();

            try
            {
             
                // Validatie: stadionvak moet geselecteerd zijn
                if (viewModel.GeselecteerdStadionvakId == 0)
                    throw new Exception("Selecteer een stadionvak.");

                // Validatie: user mag maar 1 abonnement per club kopen
                if (cart.AbonnementCarts.Any(a => a.ClubId == viewModel.ClubId))
                    throw new Exception($"Je hebt al een abonnement voor {club.Naam} in je winkelmand.");

                // Validatie: geen abonnement + los ticket voor thuismatch van dezelfde club
                var heeftConflict = cart.Carts?.Any(c => c.ThuisclubId == viewModel.ClubId) ?? false;
                if (heeftConflict)
                    throw new Exception("Je kan geen abonnement én een los ticket voor een thuismatch van dezelfde club hebben.");

            }
            catch (Exception ex)
            {
                viewModel.Club = club;
                viewModel.Stadionvakken = await _stadionvakService.GetByStadionAsync(club.StadionId);
                ModelState.AddModelError("", ex.Message);
                return View("Abonnement", viewModel);
            }

            // Validaties ok → voeg toe aan cart
            cart.AbonnementCarts.Add(new AbonnementCartItemVM
            {
                ClubId = club.Id,
                ClubNaam = club.Naam,
                ClubLogo = club.LogoPath,
                StadionvakId = viewModel.GeselecteerdStadionvakId,
                Prijs = 200m
            });

            HttpContext.Session.SetObject("ShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
    
}
