using System;
using System.Collections.Generic;
using System.Text;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Repositories.DAO
{
    public class ZitplaatsDAO : IZitplaatsDAO
    {
        private readonly ChampionsLeagueDbContext _context;

        public ZitplaatsDAO(ChampionsLeagueDbContext context)
        {
            _context = context;
        }

        //Tickets
        //Gebruiker kiest een match, een vak en het aantal gewenste zitplaatsen
        //Return: Lijst van zitplaatsen
        public async Task<IEnumerable<Zitplaats>> GetBeschikbareZitplaatsenVoorTicketAsync(int matchId, int stadionvakId, int aantalGewensteZitplaatsen)
        {
            return await _context.Zitplaatsen
                .Where(z => z.StadionvakId == stadionvakId)
                .Where(z => !z.Tickets.Any(t => t.MatchId == matchId && t.Status != "geannuleerd")) //Sluit stoelen uit waarvan tickets al gekocht zijn
                .Where(z => !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id)) //R-6 Sluit stoelen uit die een abonnement hebben
                .Take(aantalGewensteZitplaatsen)
                .ToListAsync();
        }

        //Abonnementen
        //Gebruiker kiest een vak en krijgt de eerste vrije zitplaats in dat vak toegewezen
        //Return: 1 zitplaats
        public async Task<Zitplaats?> GetBeschikbareZitplaatsVoorAbonnementAsync(int stadionvakId)
        {
           
            return await _context.Zitplaatsen
                .Where(z => z.StadionvakId == stadionvakId)
                .Where(z => !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id)) // Sluit stoelen uit die al een abonnement hebben
                .Where(z => !_context.Tickets.Any(t => t.ZitplaatsId == z.Id && t.Status != "geannuleerd")) //R-7 // Sluit stoelen uit die al een actief ticket hebben
                .FirstOrDefaultAsync();
        }

        //Get aantal vrije zitplaatsen
        //Return: int
        public async Task<int> GetAantalBeschikbareZitplaatsen(int stadionvakId, int matchId)
        {
            return await _context.Zitplaatsen
                .Where(z => z.StadionvakId == stadionvakId)
                .Where(z => !_context.Abonnements.Any(a => a.ZitplaatsId == z.Id)) //Sluit stoelen uit die een abonnement hebben
                .Where(z => matchId == 0 || !z.Tickets.Any(t => t.MatchId == matchId && t.Status != "geannuleerd")) // Get alle zitplaatsen die geen abonnement hebben of sluit tickets uit die een zitplaats hebben
                .CountAsync();
        }

    }
}
