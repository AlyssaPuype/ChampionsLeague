using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {

        private readonly ITicketService _ticketService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ITicketService ticketService, UserManager<ApplicationUser> userManager) // ← missing
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }
        
        //Toon History van tickets
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            //get tickets for the user
            var tickets = await _ticketService.GetByUserAsync(user.Id);

            return View(tickets ?? new List<Ticket>());
        }


        // Annuleer een ticket
        [HttpPost]
        public async Task<IActionResult> Annuleer(int ticketId)
        {
            try
            {
                await _ticketService.AnnuleerAsync(ticketId);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("History");
        }
    }
}
