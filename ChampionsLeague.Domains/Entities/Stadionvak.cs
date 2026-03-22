using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Stadionvak
{
    public int Id { get; set; }

    public int StadionId { get; set; }

    public string? Naam { get; set; }
    
    public string? Code { get; set; }

    public string? Ring { get; set; }

    public string? Type { get; set; }

    public string? Partij { get; set; }

    public int? Capaciteit { get; set; }

    public virtual Stadion Stadion { get; set; } = null!;

    public virtual ICollection<Zitplaats> Zitplaats { get; set; } = new List<Zitplaats>();
}
