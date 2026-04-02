namespace ChampionsLeague.Models
{
    public class ShoppingCartVM
    {
        public List<CartItemVM>? Carts { get; set; }

        public decimal ComputeTotalValue() =>
            Carts?.Sum(e => e.Prijs * e.AantalTickets) ?? 0;
    }

    public class CartItemVM
    {
        public int MatchId { get; set; }
        public string? MatchNaam { get; set; }
        public string? MatchDatum { get; set; }
        public string? StadionNaam { get; set; }
        public int StadionvakId { get; set; }
        public string? StadionvakNaam { get; set; }
        public int AantalTickets { get; set; }
        public decimal Prijs { get; set; }
    }
}