using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Models.Order
{
    public class OrderTicketVM
    {
        public IEnumerable<Match> Matches { get; set; } = new List<Match>();
        public IEnumerable<Stadionvak> Stadionvakken { get; set; } = new List<Stadionvak>();
        public IEnumerable<Zitplaats> BeschikbareZitplaatsen { get; set; } = new List<Zitplaats>();
        public int GeselecteerdMatchId { get; set; }
        public int AantalTickets { get; set; } =1;
        public int GeselecteerdStadionvakId { get; set; }
        public int GeselecteerdeZitplaatsId { get; set; }

    }
}
