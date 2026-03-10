using System;
using System.Collections.Generic;

namespace ChampionsLeague.Domains.Entities;

public partial class Match
{
    public int Id { get; set; }

    public int CompetitieId { get; set; }

    public int ThuisclubId { get; set; }

    public int BezoekersclubId { get; set; }

    public DateOnly? MatchDate { get; set; }

    public int StadionId { get; set; }

    public virtual Club Bezoekersclub { get; set; } = null!;

    public virtual Competitie Competitie { get; set; } = null!;

    public virtual Stadion Stadion { get; set; } = null!;

    public virtual Club Thuisclub { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
