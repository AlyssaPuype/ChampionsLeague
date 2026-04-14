using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Competitie
{
    public int Id { get; set; }

    public string? Naam { get; set; }

    public DateOnly? StartDatum { get; set; }
    public DateOnly? EindDatum { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
