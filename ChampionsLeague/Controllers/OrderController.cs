using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Models.Order;
using ChampionsLeague.Services;
using ChampionsLeague.Services.Services;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMatchService _matchService;
        private readonly IStadionvakService _stadionvakService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IMatchService matchService, IStadionvakService stadionvakService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _matchService = matchService;
            _stadionvakService = stadionvakService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int matchId)
        {
            var match = await _matchService.GetMatchByIdAsync(matchId);

            var vakken = await _stadionvakService.GetByStadionAsync(match.Stadion.Id);

            var viewModel = new OrderTicketVM
            {
                Match = match,
                Stadionvakken = vakken,
                GeselecteerdMatchId = matchId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(OrderTicketVM viewmodel)
        {
            var user = await _userManager.GetUserAsync(User);

            await _orderService.CreateTicketOrderAsync(user.Id, viewmodel.GeselecteerdMatchId, viewmodel.GeselecteerdStadionvakId, viewmodel.AantalTickets);

            return RedirectToAction("Index", "Home");

        }
    }
}
