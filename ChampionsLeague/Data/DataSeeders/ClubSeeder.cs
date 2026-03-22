using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;

public static class ClubSeeder
{
    public static List<Club> Seed(ChampionsLeagueDbContext context, List<Stadion> stadions)
    {
        var clubs = new List<Club>
        {
            new() { Naam = "Real Madrid",          Stadion = stadions[0], LogoPath = "/images/RealMadrid-logo.png" },
            new() { Naam = "Manchester City",      Stadion = stadions[1], LogoPath = "/images/ManCity.png" },
            new() { Naam = "Bayern München",       Stadion = stadions[2], LogoPath = "/images/Bayern-logo.png" },
            new() { Naam = "Paris Saint-Germain",  Stadion = stadions[3], LogoPath = "/images/Psg-logo.png" },
            new() { Naam = "Club Brugge",          Stadion = stadions[4], LogoPath = "/images/Brugge-logo.png" },
            new() { Naam = "FC Barcelona",         Stadion = stadions[5], LogoPath = "/images/Barca-logo.png" },
        };
        context.Clubs.AddRange(clubs);
        return clubs;
    }
}