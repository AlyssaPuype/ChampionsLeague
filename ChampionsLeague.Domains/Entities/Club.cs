using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChampionsLeague.Domains.Entities;

public partial class Club
{
    public int Id { get; set; }

    public int StadionId { get; set; }

    [MaxLength(100)]
    public string? Naam { get; set; }

    public virtual ICollection<Abonnement> Abonnements { get; set; } = new List<Abonnement>();

    public virtual ICollection<Match> MatchBezoekersclubs { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchThuisclubs { get; set; } = new List<Match>();

    public virtual Stadion Stadion { get; set; } = null!;
}
