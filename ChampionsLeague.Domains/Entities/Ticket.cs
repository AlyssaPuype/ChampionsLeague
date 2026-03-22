using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public int ZitplaatsId { get; set; }

    public int? Orderlineid { get; set; }

    public decimal Prijs { get; set; }

    public string Status { get; set; } = null!;

    public virtual Match Match { get; set; } = null!;

    public virtual Orderline? Orderline { get; set; }

    public virtual Voucher? Voucher { get; set; }

    public virtual Zitplaats Zitplaats { get; set; } = null!;
}
