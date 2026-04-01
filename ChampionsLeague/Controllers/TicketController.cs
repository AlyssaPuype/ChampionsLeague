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

        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            //get tickets for the user
            var tickets = await _ticketService.GetByUserAsync(user.Id);

            return View(tickets ?? new List<Ticket>());
        }

        [HttpPost]
        public async Task<IActionResult> Annuleer(int ticketId)
        {
            var ticket = await _ticketService.GetByIdAsync(ticketId);
            if (ticket == null) return NotFound();

            ticket.Status = "geannuleerd";
            await _ticketService.UpdateAsync(ticket);

            return RedirectToAction("History");
        }
    }
}
