using Microsoft.AspNetCore.Mvc;
using ChampionsLeague.Services.Services.Interfaces; 

namespace ChampionsLeague.Controllers
{
    public class TicketController : Controller
    {

        private readonly ITicketService _ticketService;
        public IActionResult Index()
        {
            return View();
        }
    }
}
