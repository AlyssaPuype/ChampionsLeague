using ChampionsLeague.Data;
using ChampionsLeague.Data.DAOs;
using ChampionsLeague.Data.DataSeeders;
using ChampionsLeague.Data.Interfaces;
using ChampionsLeague.Domains.DB;
using ChampionsLeague.Domains.Entities;
using ChampionsLeague.Repositories.DAO;
using ChampionsLeague.Repositories.DAO.Interfaces;
using ChampionsLeague.Services;
using ChampionsLeague.Services.Services;
using ChampionsLeague.Services.Services.Interfaces;
using ChampionsLeague.Web.DAO;
using ChampionsLeague.Util.Mail;
using ChampionsLeague.Util.Mail.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


//seeding data - source: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
builder.Services.AddDbContext<ChampionsLeagueDbContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//DAO
//Dbcontext is AddScoped by default, so for DAO too
builder.Services.AddScoped<IClubDAO, ClubDAO>();
builder.Services.AddScoped<IMatchDAO, MatchDAO>();
builder.Services.AddScoped<IOrderDAO, OrderDAO>();
builder.Services.AddScoped<IStadionvakDAO, StadionvakDAO>();
builder.Services.AddScoped<IZitplaatsDAO, ZitplaatsDAO>();
builder.Services.AddScoped<ITicketDAO, TicketDAO>();


//services
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStadionvakService, StadionvakService>();
builder.Services.AddScoped<IZitplaatsService, ZitplaatsService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();


//Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.ChampionsLeague.Session";

    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

//Email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddSingleton<IEmailSend, EmailSend>();


var app = builder.Build();

await DbSeeder.SeedAsync(app.Services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSession();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
