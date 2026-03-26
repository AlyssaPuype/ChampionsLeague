using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Controllers
{
    public class MatchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
