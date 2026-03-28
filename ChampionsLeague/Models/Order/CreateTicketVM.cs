using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Models.Order
{
    public class CreateTicketVM
    {
        public Match Match { get; set; } = null!;
        public IEnumerable<Stadionvak> Stadionvakken { get; set; } = new List<Stadionvak>();
        public IEnumerable<Zitplaats> BeschikbareZitplaatsen { get; set; } = new List<Zitplaats>();
        public int MatchId { get; set; }
        public int AantalTickets { get; set; } = 1;
        public int GeselecteerdStadionvakId { get; set; }
        public int GeselecteerdeZitplaatsId { get; set; }

    }
}
