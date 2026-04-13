Github repo link:

Stappen:

Project aanmaken met Identity

Verschillende project layers aangemaakt:
	- ChampionsLeague (Applicatie laag)
	- ChampionsLeague.Domains
	- ChampionsLeague.Repositories
	- ChampionsLeague.Services

EF core toegevoegd in de verschillende lagen

Database aangemaakt in SQL Server Management Studio

Connectiestring toegevoegd in appsettings.json

Database scaffold uitgevoerd in ChampionsLeague.Domains

Identity Migration uitgevoerd

DbSeeders aangemaakt om data in de database te zetten

DAO's aangemaakt in de repositories laag

Services aangemaakt in de services laag

Eerste controller voor Matches en Clubs aangemaakt


Issues:

- Na Submit van CreateTicket form
- Error: SqlException: The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Order_AspNetUser". The conflict occurred in database "ChampionsLeagueDB", table "dbo.ApplicationUser", column 'Id'. 
ChampionsLeagueDbContext accidentally created a separate ApplicationUser table. 
The Order table had a FK pointing to this table instead of the correct AspNetUsers table managed by Identity.
Toegevoegd: modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers") in ChampionsLeagueDbContext
ApplicationUser tabel manueel verwijderd in database.



EMPTY orders query:

DELETE FROM Voucher;
DELETE FROM Ticket;
DELETE FROM Abonnement;
DELETE FROM Orderline;
DELETE FROM [Order];

DBCC CHECKIDENT ('Voucher', RESEED, 0);
DBCC CHECKIDENT ('Ticket', RESEED, 0);
DBCC CHECKIDENT ('Abonnement', RESEED, 0);
DBCC CHECKIDENT ('Orderline', RESEED, 0);
DBCC CHECKIDENT ('[Order]', RESEED, 0);


