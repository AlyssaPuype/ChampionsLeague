// Controllers/Api/HotelApiController.cs
using ChampionsLeague.Models.Hotel;
using ChampionsLeague.Util.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeague.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelApiController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelApiController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        
        // Zoek hotels op basis van stad, checkin, checkout en aantal personen

        [HttpGet("search")]
        public async Task<IActionResult> Search(string city, string checkIn, string checkOut, int adults = 1)
        {
            if (string.IsNullOrEmpty(city))
                return BadRequest("Stad is verplicht.");

            var destinationId = await _hotelService.GetDestinationIdAsync(city);
            if (destinationId == null)
                return NotFound("Stad niet gevonden.");

            var hotels = await _hotelService.SearchHotelsAsync(destinationId, checkIn, checkOut, adults);
            return Ok(hotels);
        }

        
    }
}