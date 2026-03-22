using Microsoft.AspNetCore.Mvc;
using ChampionsLeague.Services.Services.Interfaces;


namespace ChampionsLeague.Controllers
{
    public class ClubController : Controller
    {

        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        // Get all clubs
        public async Task<IActionResult> Index()
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return View(clubs);

        }

        // Get one club by id
        public async Task<IActionResult> Detail(int id)
        {
            var club = await _clubService.GetByIdAsync(id);
            if (club == null) return NotFound();
            return View(club);
        }
    }
}
