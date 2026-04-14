using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services;
using ChampionsLeague.Services.Services.Interfaces;
using ChampionsLeague.Util.Mail.Interfaces;
using Moq;


// AbonnementService
// Business rule test: Abonnementen kunnen enkel aangekocht worden voor de start van de competitie

namespace ChampionsLeagueTests
{
    public class TestAbonnement
    {
        //Nodige services uit AbonnementService als Mock inladen
        private readonly Mock<IAbonnementDAO> _mockAbonnementDAO = new();
        private readonly Mock<IOrderDAO> _mockOrderDAO = new();
        private readonly Mock<IClubService> _mockClubService = new();
        private readonly Mock<IMatchService> _mockMatchService = new();
        private readonly Mock<IEmailSend> _mockEmailSend = new();
        private readonly Mock<ICompetitieService> _mockCompetitieService = new();
        private readonly AbonnementService _service;

        //Setup van de Mocks, voor alle tests
        public TestAbonnement()
        {
            // Club setup — geldig voor alle tests
            _mockClubService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new Club { Id = 1, Naam = "Bayern München" });

            // Zitplaats setup
            _mockAbonnementDAO.Setup(s => s.GetBeschikbareZitplaatsAsync(1))
                .ReturnsAsync(new Zitplaats { Id = 1, ZitplaatsNummer = "ODT1" });

            // OrderDAO setup
            _mockOrderDAO.Setup(s => s.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _mockOrderDAO.Setup(s => s.SaveAsync()).Returns(Task.CompletedTask);

            // Email setup
            _mockEmailSend.Setup(s => s.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _service = new AbonnementService(
                _mockAbonnementDAO.Object,
                _mockOrderDAO.Object,
                _mockClubService.Object,
                _mockMatchService.Object,
                _mockCompetitieService.Object,
                _mockEmailSend.Object
            );
        }

        //Startdatum ligt in het verleden, we roepen CreateAbonnementOrderAsync aan, er wordt een exception gegooid
        [Fact]
        public async Task CreateAbonnementOrderAsync_NaStartDatum_ThrowsException()
        {
            // Arrange
            _mockCompetitieService.Setup(s => s.GetStartDatumAsync())
                .ReturnsAsync(new DateOnly(2025, 1, 1)); // in het verleden

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _service.CreateAbonnementOrderAsync("userId", "email@test.com", 1));
        }

        //Startdatum ligt in de toekomst, we roepen CreateAbonnementOrderAsync aan, er wordt geen exception gegooid en checken of de order werd aangemaakt
        [Fact]
        public async Task CreateAbonnementOrderAsync_VoorStartDatum_MaaktAbonnementAan()
        {
            // Arrange
            _mockCompetitieService.Setup(s => s.GetStartDatumAsync())
                .ReturnsAsync(new DateOnly(2026, 4, 30)); // in de toekomst

            _mockAbonnementDAO.Setup(s => s.HeeftAbonnementVoorClubAsync("userId", 1))
                .ReturnsAsync(false);

            // Act
            await _service.CreateAbonnementOrderAsync("userId", "email@test.com", 1);

            // Assert
            _mockOrderDAO.Verify(s => s.AddAsync(It.IsAny<Order>()), Times.Once);
            _mockOrderDAO.Verify(s => s.SaveAsync(), Times.Once);
        }
    }
}