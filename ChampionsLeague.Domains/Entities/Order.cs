using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalePrijs { get; set; }

    public virtual ICollection<Orderline> Orderlines { get; set; } = new List<Orderline>();

    public virtual AspNetUser User { get; set; } = null!;
}
