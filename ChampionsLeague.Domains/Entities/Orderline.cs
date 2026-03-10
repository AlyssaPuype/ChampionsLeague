using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Orderline
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public decimal Prijs { get; set; }

    public virtual ICollection<Abonnement> Abonnements { get; set; } = new List<Abonnement>();

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
