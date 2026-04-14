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
        
        public ClubController(IClubService clubService, UserManager<ApplicationUser> userManager)
        {
            _clubService = clubService;
            _userManager = userManager;
            
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

        
    }
}