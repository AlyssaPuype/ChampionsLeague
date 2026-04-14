using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services.Interfaces;
using ChampionsLeague.Util.Mail.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services
{
    public class AbonnementService : IAbonnementService
    {

        private readonly IAbonnementDAO _abonnementDAO;
        private readonly IOrderDAO _orderDAO;
        private readonly IClubService _clubService;
        private readonly IMatchService _matchService;
        private readonly ICompetitieService _competitieService;
        private readonly IEmailSend _emailSend;




        private const decimal AbonnementPrijs = 200m;


        public AbonnementService(IAbonnementDAO abonnementDAO, IOrderDAO orderDAO, IClubService clubService, IMatchService matchService, ICompetitieService competitieService, IEmailSend emailSend)
        {
            _abonnementDAO = abonnementDAO;
            _orderDAO = orderDAO;
            _clubService = clubService;
            _matchService = matchService;
            _competitieService = competitieService;
            _emailSend = emailSend;
        }
        public async Task<bool> HeeftAbonnementVoorClubAsync(string userId, int clubId)
        {
            return await _abonnementDAO.HeeftAbonnementVoorClubAsync(userId, clubId);
        }

        public async Task CreateAbonnementOrderAsync(string userId, string email, int clubId)
        {

            var club = await _clubService.GetByIdAsync(clubId);
            if (club == null) throw new Exception("Club niet gevonden.");

            //Validation: abonnement enkel voor start competitie kopen
            //Via competitieService
            var startDatum = await _competitieService.GetStartDatumAsync();
            if (startDatum != null && DateOnly.FromDateTime(DateTime.Now) >= startDatum.Value)
                throw new Exception($"Abonnementen kunnen enkel gekocht worden vóór {startDatum.Value}.");


            //Validation: user heeft al abonnement voor deze club
            if (await _abonnementDAO.HeeftAbonnementVoorClubAsync(userId, clubId))
                throw new Exception("Je hebt al een abonnement voor deze club.");

            // zoek beschikbare zitplaats
            var zitplaats = await _abonnementDAO.GetBeschikbareZitplaatsAsync(clubId);
            if (zitplaats == null)
                throw new Exception("Geen beschikbare zitplaatsen meer voor dit abonnement.");

            // maak orderline + abonnement
            var orderline = new Orderline { Prijs = AbonnementPrijs };
            orderline.Abonnements.Add(new Abonnement
            {
                ClubId = clubId,
                ZitplaatsId = zitplaats.Id,
                Prijs = AbonnementPrijs
            });

            var order = new Order
            {
                UserId = userId,
                Orderlines = new List<Orderline> { orderline },
                TotalePrijs = AbonnementPrijs,
                OrderDate = DateTime.Now
            };

            await _orderDAO.AddAsync(order);
            await _orderDAO.SaveAsync();

            await _emailSend.SendEmailAsync(
                email,
                "Bevestiging abonnement - ChampionsLeague",
                $@"<h2>Bedankt voor uw abonnement!</h2>
               <p>U heeft een abonnement gekocht voor alle thuismatches van <strong>{club.Naam}</strong> .</p>
               <p>Zitplaats: <strong>{zitplaats.ZitplaatsNummer}</strong></p>
               <p>Prijs: €{AbonnementPrijs}</p>"
            );
        }

        public async Task<IEnumerable<Abonnement>> GetByUserIdAsync(string userId)
        {
            return await _abonnementDAO.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Abonnement>> GetAllAsync()
        {
            return await _abonnementDAO.GetAllAsync();
        }

        public async Task<Abonnement?> GetByIdAsync(int id)
        {
            return await _abonnementDAO.GetByIdAsync(id);
        }
    }
}
