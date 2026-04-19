using System;
using System.Collections.Generic;
using System.Text;

//data properties van een hotel (zie api)
namespace ChampionsLeague.Util.Hotel.Models
{
    public class HotelResultVM
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public double? ReviewScore { get; set; }
        public int? ReviewCount { get; set; }
        public string? ReviewWord { get; set; }
        public double? Stars { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
    }
}
