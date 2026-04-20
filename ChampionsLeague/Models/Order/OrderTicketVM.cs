using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Services.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ChampionsLeague.Models.Order
{
    public class OrderTicketVM
    {
        //Dit hoeft geen validatie
        [ValidateNever]
        public Match Match { get; set; } = null!;

        //Dit hoeft geen validatie
        [ValidateNever]
        public IEnumerable<Stadionvak> Stadionvakken { get; set; } = new List<Stadionvak>();
        public int GeselecteerdMatchId { get; set; }
        public int AantalTickets { get; set; } =1;
        public int GeselecteerdStadionvakId { get; set; }
        public decimal Prijs { get; set; } = Prijzen.TicketPrijs;


    }
}
