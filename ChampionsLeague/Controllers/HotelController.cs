using ChampionsLeague.Util.Hotel;
using Microsoft.AspNetCore.Mvc;
using ChampionsLeague.Models.Hotel;

namespace ChampionsLeague.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public IActionResult Index()
        {
            return View(new HotelSearchVM());
        }


        //Zoek naar een hotel, bind parameters aan de viewmodel (deze geeft het door aan de service die de API roept)
        //Resultaat in viewmodel steken en returnen
        [HttpPost]
        public async Task<IActionResult> Search(HotelSearchVM model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            var destinationId = await _hotelService.GetDestinationIdAsync(model.City);
            if (destinationId == null)
            {
                ModelState.AddModelError("", $"Geen bestemming gevonden voor '{model.City}'.");
                return View("Index", model);
            }

            model.Results = await _hotelService.SearchHotelsAsync(
                destinationId, model.CheckIn, model.CheckOut, model.Adults);

            return View("Index", model);
        }
    }
}
