using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services.Services;
using ChampionsLeague.Services.Services.Interfaces;
using Moq;


//TicketService
//Business rule test: Tickets kunnen enkel gratis geannuleerd worden 1 week voor de match.

namespace ChampionsLeagueTests
{
    public class TestTicketAnnulatie
    {
        [Fact]
        public async Task AnnuleerAsync_MeerDanEenWeekVoorMatch_Annuleert()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 1,
                Status = "gereserveerd",
                Match = new ChampionsLeague.Domains.Entities.Match { MatchDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10)) } 
            };

            var mockTicketDAO = new Mock<ITicketDAO>();
            mockTicketDAO.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(ticket);
            mockTicketDAO.Setup(s => s.UpdateAsync(It.IsAny<Ticket>())).Returns(Task.CompletedTask);
            mockTicketDAO.Setup(s => s.SaveAsync()).Returns(Task.CompletedTask);

            var service = new TicketService(mockTicketDAO.Object);

            // Act
            await service.AnnuleerAsync(1);

            // Assert
            Assert.Equal("geannuleerd", ticket.Status);
            mockTicketDAO.Verify(s => s.UpdateAsync(It.IsAny<Ticket>()), Times.Once);
            mockTicketDAO.Verify(s => s.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task AnnuleerAsync_MinderDanEenWeekVoorMatch_ThrowsException()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 1,
                Status = "gereserveerd",
                Match = new ChampionsLeague.Domains.Entities.Match { MatchDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)) } 
            };

            var mockTicketDAO = new Mock<ITicketDAO>();
            mockTicketDAO.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(ticket);

            var service = new TicketService(mockTicketDAO.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.AnnuleerAsync(1));
        }
    }
}
