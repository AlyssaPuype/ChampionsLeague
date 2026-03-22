using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

public static class ClubSeeder
{
    public static List<Club> Seed(ChampionsLeagueDbContext context, List<Stadion> stadions)
    {
        var clubs = new List<Club>
        {
            new() { Naam = "Real Madrid",          Stadion = stadions[0] },
            new() { Naam = "Manchester City",       Stadion = stadions[1] },
            new() { Naam = "Bayern München",        Stadion = stadions[2] },
            new() { Naam = "Paris Saint-Germain",   Stadion = stadions[3] },
            new() { Naam = "Club Brugge",           Stadion = stadions[4] },
            new() { Naam = "FC Barcelona",          Stadion = stadions[5] },
        };
        context.Clubs.AddRange(clubs);
        return clubs;
    }
}