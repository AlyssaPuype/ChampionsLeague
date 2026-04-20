using ChampionsLeague.Util.Hotel.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


// Hotel service flow:
//Todo: User boekt een ticket, krijgt op de orderpagina een mogelijkheid om ook een hotel te boeken (bestemming automatisch geselecteerd door te vergelijken
//met stad van thuisclub in de match. (to do: property toevoegen aan de entiteit, migratie uitvoeren) 
//Waneeer geen hotel is gekozen, enkel ticket toevoegen aan shoppingcart, wanneer wel toegevoegd, hotel in shoppingcart
//Bij bevestiging, kan de gebruiker de hotelboeking zien onder profiel -> hotelboekingen met info: stad, datum, prijs, aantal personen

//Momenteel: hotelboeking apart via navigatiemenu "hotels". Bij boeking komt de boeking in profielhistory te staan.

namespace ChampionsLeague.Util.Hotel
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiHost;

        //IConfiguration: ASPN.NET built in interface -> reads appsettings.json
        public HotelService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["RapidApi:Key"]!;
            _apiHost = config["RapidApi:BookingHost"]!;


            //In postman, headers manueel ingevoerd. In de applicatie: via controller toevoegen
            //source: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.defaultrequestheaders?view=net-10.0
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _apiHost);
        }

        //Get Destination Ids
        public async Task<string?> GetDestinationIdAsync(string city)
        {
            //Building URL + Speciale karakters converten
            var url = $"https://{_apiHost}/v1/hotels/locations?name={Uri.EscapeDataString(city)}&locale=en-gb";

            //GET Request sturen
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            //JSON lezen
            var json = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(json).RootElement;

            //Check if JSON is een array en zoek naar dest_id
            if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
            {
                var first = root[0];
                if (first.TryGetProperty("dest_id", out var destId))
                    return destId.GetString();
            }

            return null;
        }


        //Get list of hotels
        public async Task<List<HotelResultVM>> SearchHotelsAsync(string destinationId, string checkIn, string checkOut, int adults)
        {

            //Building URL
            var url = $"https://{_apiHost}/v1/hotels/search" +
                      $"?dest_id={destinationId}" +
                      $"&dest_type=city" +
                      $"&checkin_date={checkIn}" +
                      $"&checkout_date={checkOut}" +
                      $"&adults_number={adults}" +
                      $"&room_number=1" +
                      $"&locale=en-gb" +
                      $"&currency=EUR" +
                      $"&order_by=popularity" +
                      $"&filter_by_currency=EUR" +
                      $"&units=metric";

            var response = await _httpClient.GetAsync(url);

            //Error handling: geen response -> empty list
            if (!response.IsSuccessStatusCode) return new List<HotelResultVM>();

            var json = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(json).RootElement;

            var hotels = new List<HotelResultVM>();

            //API returns hotels in a "result" array
            if (!root.TryGetProperty("result", out var results)) return hotels;


            //Loop through JSON array
            //source: https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonelement.enumeratearray?view=net-10.0
            foreach (var hotel in results.EnumerateArray())
            {
                //Map hotel aan VM
                //source: https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonelement.trygetproperty?view=net-10.0
                 hotels.Add(new HotelResultVM
                {
                    HotelId = hotel.TryGetProperty("hotel_id", out var id)
                        ? id.GetInt32() : 0,
                    Name = hotel.TryGetProperty("hotel_name", out var naam)
                        ? naam.GetString() ?? "Unknown" : "Unknown",
                    Address = hotel.TryGetProperty("address", out var adres)
                        ? adres.GetString() : null,
                    City = hotel.TryGetProperty("city", out var stad)
                        ? stad.GetString() : null,
                    Country = hotel.TryGetProperty("country_trans", out var land)
                        ? land.GetString() : null,
                    ReviewScore = hotel.TryGetProperty("review_score", out var score) && score.ValueKind != JsonValueKind.Null
                        ? score.GetDouble() : null,
                    ReviewCount = hotel.TryGetProperty("review_nr", out var nr) && score.ValueKind != JsonValueKind.Null
                        ? nr.GetInt32() : null,
                    ReviewWord = hotel.TryGetProperty("review_score_word", out var word)
                        ? word.GetString() : null,
                    Stars = hotel.TryGetProperty("class", out var stars) && score.ValueKind != JsonValueKind.Null
                        ? stars.GetDouble() : null,
                    PhotoUrl = hotel.TryGetProperty("main_photo_url", out var foto)
                        ? foto.GetString()?.Replace("square60", "square500") : null,
                    Url = hotel.TryGetProperty("url", out var hotelUrl)
                        ? hotelUrl.GetString() : null
                });
            }

            return hotels;
        }
    }
}
