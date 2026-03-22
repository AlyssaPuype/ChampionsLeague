using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Zitplaats
{
    public int Id { get; set; }

    public int StadionvakId { get; set; }

    public int? Nummer { get; set; }

    public virtual Abonnement? Abonnement { get; set; }

    public virtual Stadionvak Stadionvak { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
