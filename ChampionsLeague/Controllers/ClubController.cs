using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Extensions;
using ChampionsLeague.Models;
using ChampionsLeague.Models.Order;
using ChampionsLeague.Services.Services;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ChampionsLeague.Controllers
{
    public class ClubController : Controller
    {

        private readonly IClubService _clubService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAbonnementService _abonnementService;
        private readonly IStadionvakService _stadionvakService;


        public ClubController(IClubService clubService, UserManager<ApplicationUser> userManager, IAbonnementService abonnementService, IStadionvakService stadionvakService)
        {
            _clubService = clubService;
            _userManager = userManager;
            _abonnementService = abonnementService;
            _stadionvakService = stadionvakService;
        }

        // Get all clubs
        public async Task<IActionResult> Index()
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return View(clubs);

        }

        // Get one club by naam
        public async Task<IActionResult> Detail(string naam)
        {
            var club = await _clubService.GetByNaamAsync(naam);
            if (club == null) return NotFound();
            return View(club);
        }

        [HttpGet]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        [ActionName("AddAbonnementToCart")]
        public async Task<IActionResult> AddAbonnementToCartPost(OrderAbonnementVM viewModel)
        {
            var club = await _clubService.GetByIdAsync(viewModel.ClubId);
            if (club == null) return NotFound();

            var cart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart")
                ?? new ShoppingCartVM();

            cart.AbonnementCarts ??= new List<AbonnementCartItemVM>();

            if (cart.AbonnementCarts.Any(a => a.ClubId == viewModel.ClubId))
            {
                TempData["Error"] = "Je hebt al een abonnement voor deze club in je winkelmand.";
                return RedirectToAction("Abonnement", new { clubId = viewModel.ClubId });
            }

            var heeftConflict = cart.Carts?.Any(c => c.ThuisclubId == viewModel.ClubId) ?? false;
            if (heeftConflict)
            {
                TempData["Error"] = "Je kan geen abonnement én een los ticket voor een thuismatch van dezelfde club hebben.";
                return RedirectToAction("Abonnement", new { clubId = viewModel.ClubId });
            }

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