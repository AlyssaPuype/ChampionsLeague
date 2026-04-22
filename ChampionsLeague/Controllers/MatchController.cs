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
            var matches = clubId.HasValue //Als club een waarde heeft
                ? await _matchService.GetMatchesByClubAsync(clubId.Value) //haal matches op voor club
                : await _matchService.GetAllMatchesAsync(); //anders haal alle matches 

            var clubs = await _clubService.GetAllClubsAsync();
            ViewBag.Clubs = new SelectList(clubs, "Id", "Naam", clubId);
            return View(matches);
        }


        // get matches for club (clubkalender)
        // return partial view voor unobtrusive ajax
        public async Task<IActionResult> FilterByClub(int? clubId)
        {
            var matches = clubId.HasValue
                ? await _matchService.GetMatchesByClubAsync(clubId.Value)
                : await _matchService.GetAllMatchesAsync();

            return PartialView("_MatchList", matches);
        }
    }
}
