using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Extensions;
using ChampionsLeague.Models;
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

        public ClubController(IClubService clubService, UserManager<ApplicationUser> userManager, IAbonnementService abonnementService)
        {
            _clubService = clubService;
            _userManager = userManager;
            _abonnementService = abonnementService;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAbonnementToCart(int clubId)
        {
            var club = await _clubService.GetByIdAsync(clubId);
            if (club == null) return NotFound();

            var cart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart")
                ?? new ShoppingCartVM();

            cart.AbonnementCarts ??= new List<AbonnementCartItemVM>();

            // check already in cart
            if (cart.AbonnementCarts.Any(a => a.ClubId == clubId))
            {
                TempData["Error"] = "Je hebt al een abonnement voor deze club in je winkelmand.";
                return RedirectToAction("Detail", new { naam = club.Naam }); // ← fix
            }

            // check conflict
            var heeftConflict = cart.Carts?.Any(c => c.ThuisclubId == clubId) ?? false;
            if (heeftConflict)
            {
                TempData["Error"] = "Je kan geen abonnement én een los ticket voor een thuismatch van dezelfde club hebben.";
                return RedirectToAction("Detail", new { naam = club.Naam }); // ← fix
            }

            cart.AbonnementCarts.Add(new AbonnementCartItemVM
            {
                ClubId = club.Id,
                ClubNaam = club.Naam,
                ClubLogo = club.LogoPath,
                Prijs = 200m
            });

            HttpContext.Session.SetObject("ShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }


    }
}
