using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Abonnement
{
    public int Id { get; set; }

    public int ClubId { get; set; }

    public int ZitplaatsId { get; set; }

    public int? Orderlineid { get; set; }

    public decimal Prijs { get; set; }

    public virtual Club Club { get; set; } = null!;

    public virtual Orderline? Orderline { get; set; }

    public virtual Zitplaats Zitplaats { get; set; } = null!;
}
