using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Models.Order
{
    public class OrderTicketVM
    {
        public Match Match { get; set; } = null!;
        public IEnumerable<Stadionvak> Stadionvakken { get; set; } = new List<Stadionvak>();
        public int GeselecteerdMatchId { get; set; }
        public int AantalTickets { get; set; } =1;
        public int GeselecteerdStadionvakId { get; set; }
       

    }
}
