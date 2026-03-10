using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Voucher
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string Code { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
