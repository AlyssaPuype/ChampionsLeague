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

            // Speeldag 1 - 01/04/2026 
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[1], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 1) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[3], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 1) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[5], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 1) });

            // Speeldag 2 - 08/04/2026
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[0], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 8) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[2], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 8) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[4], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 8) });

            // Speeldag 3 - 15/04/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[2], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 15) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[4], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 15) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[5], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 15) });

            // Speeldag 4 - 22/04/2026
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[0], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 22) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[1], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 22) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[3], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 22) });

            // Speeldag 5 - 29/04/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[3], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 29) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[5], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 29) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[4], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 4, 29) });

            // Speeldag 6 - 06/05/2026
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[0], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 6) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[1], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 6) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[2], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 6) });

            // Speeldag 7 - 13/05/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[4], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 13) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[3], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 13) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[5], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 13) });

            // Speeldag 8 - 20/05/2026
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[0], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 20) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[1], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 20) });
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[2], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 20) });

            // Speeldag 9 - 27/05/2026
            matches.Add(new Match { Thuisclub = clubs[0], Bezoekersclub = clubs[5], Stadion = clubs[0].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 27) });
            matches.Add(new Match { Thuisclub = clubs[1], Bezoekersclub = clubs[2], Stadion = clubs[1].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 27) });
            matches.Add(new Match { Thuisclub = clubs[3], Bezoekersclub = clubs[4], Stadion = clubs[3].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 5, 27) });

            // Speeldag 10 - 03/06/2026
            matches.Add(new Match { Thuisclub = clubs[5], Bezoekersclub = clubs[0], Stadion = clubs[5].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 3) });
            matches.Add(new Match { Thuisclub = clubs[2], Bezoekersclub = clubs[1], Stadion = clubs[2].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 3) });
            matches.Add(new Match { Thuisclub = clubs[4], Bezoekersclub = clubs[3], Stadion = clubs[4].Stadion, Competitie = competitie, MatchDate = new DateOnly(2026, 6, 3) });

            context.Matches.AddRange(matches);
            return matches;
        }
    }
}