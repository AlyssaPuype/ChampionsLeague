using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Extensions;
using ChampionsLeague.Models;
using ChampionsLeague.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace ChampionsLeague.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            return View(cartList);
        }
        public IActionResult Delete(int? matchId)
        {
            if (matchId == null) return NotFound();

            ShoppingCartVM? cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            CartItemVM? itemToRemove =
                cartList?.Carts?.FirstOrDefault(r => r.MatchId == matchId);

            if (itemToRemove != null)
            {
                cartList?.Carts?.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            return View("Index", cartList);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTicket()
        {
            var user = await _userManager.GetUserAsync(User);
            var carts = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            try
            {
                if (carts.Carts != null)
                {
                    foreach (var cart in carts.Carts)
                    {
                        await _orderService.CreateTicketOrderAsync(
                            user!.Id,
                            user!.Email,
                            cart.MatchId,
                            cart.StadionvakId,
                            cart.AantalTickets
                        );
                    }
                }

                // clear cart after payment
                HttpContext.Session.Remove("ShoppingCart");
                TempData["Success"] = "Bestelling geplaatst!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View("Index", carts);
            }

            return RedirectToAction("History", "Ticket");
        }
    }
}
