using ChampionsLeague.Util.Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

//Form model
namespace ChampionsLeague.Models.Hotel
{
    public class HotelSearchVM
    {
        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string CheckIn { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

        [Required]
        public string CheckOut { get; set; } = DateTime.Today.AddDays(10).ToString("yyyy-MM-dd");

        [Range(1, 4)]
        public int Adults { get; set; } = 1;

        public List<HotelResultVM> Results { get; set; } = new();
    }
}
