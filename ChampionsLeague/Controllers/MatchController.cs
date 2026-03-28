using ChampionsLeague.Services.Services.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Web.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // get matches
        public async Task<IActionResult> Index()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return View(matches);
        }

        // get match by id
        public async Task<IActionResult> Detail(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }
    }
}
