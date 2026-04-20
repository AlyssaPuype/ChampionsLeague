using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChampionsLeague.Domains.Entities;

public partial class Stadion
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Naam { get; set; }

    public int Capaciteit { get; set; }

    public string? Stad { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<Stadionvak> Stadionvaks { get; set; } = new List<Stadionvak>();
}
