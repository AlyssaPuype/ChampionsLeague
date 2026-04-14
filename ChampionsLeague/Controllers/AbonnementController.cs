using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Services.Services.Interfaces;

namespace ChampionsLeague.Controllers
{

    [Authorize]
    public class AbonnementController : Controller
    {
        private readonly IAbonnementService _abonnementService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AbonnementController(IAbonnementService abonnementService, UserManager<ApplicationUser> userManager)
        {
            _abonnementService = abonnementService;
            _userManager = userManager;
        }


        // Toon history van Abonnementen
            public async Task<IActionResult> History()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var abonnementen = await _abonnementService.GetByUserIdAsync(user.Id);
            return View(abonnementen);
        }
    }
}
