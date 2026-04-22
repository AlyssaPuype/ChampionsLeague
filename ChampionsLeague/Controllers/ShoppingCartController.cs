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
    [Authorize]
    public class ShoppingCartController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;


        public ShoppingCartController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        //Toon Shoppingcart items
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            return View(cartList);
        }

        //Behandel de bestelling, maak een order
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var carts = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            try
            {
                // Abonnementen aanmaken
                if (carts?.AbonnementCarts != null)
                {
                    foreach (var abonnement in carts.AbonnementCarts)
                    {
                        if (user == null)
                        {
                            return Unauthorized();
                        }
                        await _orderService.CreateAbonnementOrderAsync(
                            user.Id,
                            user.Email,
                            abonnement.ClubId,
                            abonnement.StadionvakId
                        );
                    }
                }

                //Tickets aanmaken

                if (carts?.Carts != null)
                {
                    foreach (var cart in carts.Carts)
                    {
                        if (user == null)
                        {
                            return Unauthorized();
                        }
                        await _orderService.CreateTicketOrderAsync(
                            user.Id,
                            user.Email,
                            cart.MatchId,
                            cart.StadionvakId,
                            cart.AantalTickets
                        );
                    }
                }

                //Cart leegmaken na bestelling
                HttpContext.Session.Remove("ShoppingCart");
                TempData["Success"] = "Bestelling geplaatst!";
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
                //Uncomment om de exacte error te zien
                //var inner = ex.InnerException?.Message ?? ex.Message;
                //TempData["Error"] = inner;
                return View("Index", carts);
            }

            //Na de bestelling is geplaatst, ga naar tickets of abonnementen
            if (carts?.AbonnementCarts?.Any() == true)
                return RedirectToAction("History", "Abonnement");

            return RedirectToAction("History", "Ticket");

        }

        // Verwijder Ticket uit de session cart
        [HttpPost]
        public IActionResult DeleteTicket(int? matchId, int? stadionvakId)
        {
            if (matchId == null) return NotFound();

            ShoppingCartVM? cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            CartItemVM? itemToRemove = cartList?.Carts?.FirstOrDefault(r =>
                r.MatchId == matchId && r.StadionvakId == stadionvakId);

            if (itemToRemove != null)
            {
                cartList?.Carts?.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            return View("Index", cartList);
        }

        // verwijder Abonnement uit de session cart
        [HttpPost]
        public IActionResult DeleteAbonnement(int clubId)
        {
            var cart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            if (cart == null || cart.AbonnementCarts == null)
            {
                return View("Index", cart);
            }


            var item = cart?.AbonnementCarts?.FirstOrDefault(a => a.ClubId == clubId);
            if (item == null)
            {
                return View("Index", cart);
            }

            cart.AbonnementCarts.Remove(item);
            HttpContext.Session.SetObject("ShoppingCart", cart);


            return View("Index", cart);
        }
              
        
    }
}
