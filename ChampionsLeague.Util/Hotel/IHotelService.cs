using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Util.Hotel.Models;


namespace ChampionsLeague.Util.Hotel
{
    public interface IHotelService
    {
        Task<string?> GetDestinationIdAsync(string city);
        Task<List<HotelResultVM>> SearchHotelsAsync(string destinationId, string checkIn, string checkOut, int adults);
    }
}
