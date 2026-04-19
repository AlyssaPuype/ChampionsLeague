using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChampionsLeague.Web.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly IClubService _clubService;

        public MatchController(IMatchService matchService, IClubService clubService)
        {
            _matchService = matchService;
            _clubService = clubService;
        }

        // get matches
        public async Task<IActionResult> Index(int? clubId)
        {
            var matches = clubId.HasValue
                ? await _matchService.GetMatchesByClubAsync(clubId.Value)
                : await _matchService.GetAllMatchesAsync();

            var clubs = await _clubService.GetAllClubsAsync();
            ViewBag.Clubs = new SelectList(clubs, "Id", "Naam", clubId);
            return View(matches);
        }

        // get match by id
        public async Task<IActionResult> Detail(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }

        // get matches for club (clubkalender) returning a partial view to work with unobtrusive ajax
        [HttpGet]
        public async Task<IActionResult> FilterByClub(int? clubId)
        {
            var matches = clubId.HasValue
                ? await _matchService.GetMatchesByClubAsync(clubId.Value)
                : await _matchService.GetAllMatchesAsync();

            return PartialView("_MatchList", matches);
        }
    }
}
