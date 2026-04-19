Bevat notities en stappen in grote lijnen

Github repo link: https://github.com/AlyssaPuype/ChampionsLeague.git

#Stappen:

1.Project aanmaken met Identity

2.Verschillende project layers aangemaakt:
	- ChampionsLeague (Applicatie laag)
	- ChampionsLeague.Domains
	- ChampionsLeague.Repositories
	- ChampionsLeague.Services

3.EF core toegevoegd in de verschillende lagen

4.Database aangemaakt in SQL Server Management Studio

5.Connectiestring toegevoegd in appsettings.json

6.Database scaffold uitgevoerd in ChampionsLeague.Domains

7.Identity Migration uitgevoerd

8.DbSeeders aangemaakt om data in de database te zetten

9.Eerste DAO's aangemaakt in de repositories laag

10.Services aangemaakt in de services laag

11.Eerste controller voor Matches en Clubs aangemaakt

12.Pagina's aangemaakt om matches en clubs te tonen, klein beetje UI toegevoegd vanuit Mobile first perspectief

13.Orderservice toegevoegd en viewmodels

14.Zitplaatsservice toegevoegd

15.DataTables library toegevoegd om matches te tonen in tabel

16.Ticketservice toegevoegd (zonder abonnement momenteel)

17.Shoppingcart toegevoegd met session storage, Orderservice voor tickets werd aangepast, omdat we nu een shoppingcart hebben ipv dat de order meteen geplaatst is

18.EmailService toegevoegd

19.AbonnementService toegevoegd

20.Aanpassingen aan de workflow gemaakt voor abonnementen. Werd eerst door clubcontroller behandeld, maar naar ordercontroller verhuisd voor consistentie met tickets

21. Competitie Entity aangepast zodat deze nu startdatum en einddatum heeft, zodat met kan controleren of men een abonnement voor of na de competitie wilt aankopen

22. xUnittest geschreven om bovenstaande business rule te testen

23. Onnodige DAO en service methodes verwijderd

24. Ordering in tabel van matches gefixt

25. Capaciteit per vak tonen tijdens het orderen van tickets/abonnementen mbv AJAX




Issues:

- Na Submit van CreateTicket form
- Error: SqlException: The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Order_AspNetUser". The conflict occurred in database "ChampionsLeagueDB", table "dbo.ApplicationUser", column 'Id'. 
ChampionsLeagueDbContext accidentally created a separate ApplicationUser table. 
The Order table had a FK pointing to this table instead of the correct AspNetUsers table managed by Identity.
Toegevoegd: modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers") in ChampionsLeagueDbContext
ApplicationUser tabel manueel verwijderd in database.


## Queries en migrations commands

### EMPTY orders query:

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


### CLEAN database:
Note: Wanneer  de app opnieuw runt voor de eerste keer, kan dit wat tijd nemen omdat we veel seeders hebben, check output

	DELETE FROM [Match];
	DELETE FROM Competitie;
	DELETE FROM Zitplaats;
	DELETE FROM Stadionvak;
	DELETE FROM Club;
	DELETE FROM Stadion;

	DBCC CHECKIDENT ('Match', RESEED, 0);
	DBCC CHECKIDENT ('Competitie', RESEED, 0);
	DBCC CHECKIDENT ('Zitplaats', RESEED, 0);
	DBCC CHECKIDENT ('Stadionvak', RESEED, 0);
	DBCC CHECKIDENT ('Club', RESEED, 0);
	DBCC CHECKIDENT ('Stadion', RESEED, 0);

### ADD migrations:
	
	- ChampionsLeagueDbContext:
	Add-Migration [nameOfTheMigration] -Context ChampionsLeagueDbContext
	Update-Database -Context ChampionsLeagueDbContext

	- ApplicationDbContext:
	Add-Migration [nameOfTheMigration] -Context ApplicationDbContext
	Update-Database -Context ApplicationDbContext

### Database tables script export
- rechtsklik database
- Tasks -> Generate Scripts
- Save as script file


## Unit tests

### xUnitTest: TestAbonnement:

- create xUnit project
- references toevoegen (domains, services)
- installeer moq nuget package

### xUnitTest: TestTicketAnnulatie


Search references (handig wanneer je een bepaalde methode zoekt) :
- selecteer methode (bv in Interface klasses van services) en press shift + f12

## Add Localization:
source: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization/provide-resources?view=aspnetcore-10.0

	- Provide resources:
		- Maak folder in web project: Resources
		- Add item -> Kies resource file als template, maak voor elke taal resource file aan, voeg key en waarde toe
	- Configureeer localization in Program.cs:
	- Voeg toe in views:
		- @inject IViewLocalizer Localizer

## Hotel Booking API werkwijze:
source: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-10.0
source: https://learning.postman.com/docs/getting-started/overview

-	Account in Postman en RapidApi aangemaakt (vives account gebruikt)
-	Subscribe API in rapidApi: https://rapidapi.com/tipsters/api/booking-com/playground/apiendpoint_a159b494-e4ec-4134-8a71-5ece6d8cc60b
-	In appsettings.developments.json: bookingAPI key toegevoegd (x-rapidAPI-key)
-	In ChampionsLeague.Util: Maak folder Hotel aan en voeg HotelService en IHotelService toe
-	Register services in Program.cs en register:
	- builder.Services.AddHttpClient<IHotelService, HotelService>();

	
### Installeer Postman Desktop
-	Maak nieuw request aan
-	Vanuit RapidApi, kopieer code snippet HTTP: GET /v1/hotels/data?hotel_id=1377073&locale=en-gb
-	Plak deze in Postman, voeg header toe met key: 
	- X-RapidAPI-Key:
	- X-RapidAPI-Host:
	- Content-type: application/json
- Params zijn automatisch ingevuld
- Klik op Send en check response
- In applicatie 
	- Viewmodel op op basis van response properties
	- Hotelcontroller en view schrijven

### Flow aanpassing:
Momenteel is er een aparte pagina voor hotels, maar de gebruiker kan een hotel "boeken" zonder betaling, dus toevoegen aan shoppingcart
Bij bevestiging komt de hotelinfo als een boeking in de history te zien
- Er moet een hotelboeking entity aangemaakt worden met dezelfde kolommen als de api properties 
- Hotelboeking moet toegevoegd worden aan orderline

