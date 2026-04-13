namespace ChampionsLeague.Models
{
    public class ShoppingCartVM
    {
        public List<CartItemVM>? Carts { get; set; }

        public List<AbonnementCartItemVM>? AbonnementCarts { get; set; }


        public decimal ComputeTotalValue()
        {
            var ticketTotal = Carts?.Sum(e => e.Prijs * e.AantalTickets) ?? 0;
            var abonnementTotal = AbonnementCarts?.Sum(e => e.Prijs) ?? 0;
            return ticketTotal + abonnementTotal;
        }
    }

    public class CartItemVM
    {
        public int MatchId { get; set; }
        public int ThuisclubId { get; set; }      

        public string? MatchNaam { get; set; }
        public string? MatchDatum { get; set; }
        public string? StadionNaam { get; set; }
        public int StadionvakId { get; set; }
        public string? StadionvakNaam { get; set; }
        public int AantalTickets { get; set; }
        public decimal Prijs { get; set; }
    }

    public class AbonnementCartItemVM  
    {
        public int ClubId { get; set; }
        public string? ClubNaam { get; set; }
        public string? ClubLogo { get; set; }
        public int StadionvakId { get; set; }
        public decimal Prijs { get; set; }
    }
}