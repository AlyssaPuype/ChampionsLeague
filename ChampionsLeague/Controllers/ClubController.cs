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
        public IActionResult Index()
        {
            return View();
        }
    }
}
