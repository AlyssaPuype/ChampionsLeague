using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChampionsLeague.Domains.Entities;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalePrijs { get; set; }

    public string UserId { get; set; } = null!;

    public virtual ICollection<Orderline> Orderlines { get; set; } = new List<Orderline>();

    public virtual IdentityUser User { get; set; } = null!;
}
