# STARTUP PROJECT

## Project vereisten:

- Visual Studio 2026 18.4.3
- SQL Server Management Studio (SSMS) 22.2.1
- Lokaal SQL Server
- .NET 10 SDK

## Database setup in SQL Server Management Studio (22.2.1)

#### 1. Database script uitvoeren

1. Open SSMS en verbind met je lokale SQL Server
2. Via File > Open > File en selecteer DatabaseScripts/DbScriptWithoutMigrations.sql
3. Klik op execute
1. Database "ChampionsLeagueDB" wordt aangemaakt met de nodige tabellen

Indien er issues zijn met de migraties -> voer `fullDBScript.sql` uit (script zonder EFmigrations)
en commenteer line19 uit ChampionsLeague.Web/Data/DbSeeder.cs: //await context.Database.MigrateAsync();


#### 2. Adapt appsettings.development.json

1. Open `appsettings.Development.json` en pas de `DefaultConnection` aan

#### 3. Run project

1. Open de solution in Visual Studio 2022
2. Stel `ChampionsLeague` in als startup project
3. Run het project (F5)
4. De seeder vult automatisch de database met testdata

