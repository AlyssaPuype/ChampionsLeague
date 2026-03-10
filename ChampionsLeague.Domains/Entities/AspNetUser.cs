using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class AspNetUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
