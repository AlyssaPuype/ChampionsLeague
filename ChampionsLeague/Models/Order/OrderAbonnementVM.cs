using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Services.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ChampionsLeague.Models.Order
{
    public class OrderAbonnementVM
    {
        [ValidateNever]
        public Club Club { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<Stadionvak> Stadionvakken { get; set; } = new List<Stadionvak>();

        public int ClubId { get; set; }
        public int GeselecteerdStadionvakId { get; set; }

        public decimal Prijs { get; set; } = Prijzen.AbonnementPrijs;

    }
}