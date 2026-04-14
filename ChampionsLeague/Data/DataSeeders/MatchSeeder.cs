using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

namespace ChampionsLeague.Data.DataSeeders
{
    public static class MatchSeeder
    {
        public static List<Match> Seed(ChampionsLeagueDbContext context, List<Club> clubs, List<Competitie> competities)
        {

            // Matches for competition ( only Champions League for now)
            var matches = new List<Match>();
            var competitie = competities[0];

            // Real Madrid = clubs[0],
            // Manchester City = clubs[1],
            // Bayern München = clubs[2]
            // PSG = clubs[3],
            // Club Brugge = clubs[4],
            // FC Barcelona = clubs[5]

            // Speeldag 1 - 01/05/2026 
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[1], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 1) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[3], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 1) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[5], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 1) });

            // Speeldag 2 - 08/05/2026
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[0], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 8) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[2], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 8) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[4], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 8) });

            // Speeldag 3 - 15/05/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[2], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 15) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[4], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 15) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[5], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 15) });

            // Speeldag 4 - 22/05/2026
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[0], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 22) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[1], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 22) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[3], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 22) });

            // Speeldag 5 - 29/05/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[3], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 29) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[5], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 29) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[4], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 29) });

            // Speeldag 6 - 05/06/2026
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[0], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 5) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[1], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 5) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[2], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 5) });

            // Speeldag 7 - 12/06/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[4], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 12) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[3], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 12) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[5], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 12) });

            // Speeldag 8 - 19/06/2026
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[0], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 19) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[1], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 19) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[2], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 19) });

            // Speeldag 9 - 26/06/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[5], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 26) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[2], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 26) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[4], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 26) });

            // Speeldag 10 - 03/07/2026
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[0], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 7, 3) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[1], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 7, 3) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[3], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 7, 3) });

            context.Matches.AddRange(matches);
            return matches;
        }
    }
}