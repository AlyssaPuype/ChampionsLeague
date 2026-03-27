using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
