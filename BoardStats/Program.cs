using BoardStats.Data;
using BoardStats.Data.Services;
using BoardStats.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGamesService, GamesService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<IMatchesServices, MatchesServices>();
builder.Services.AddScoped<IWinConService, WinConService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<ICalendarServices, CalendarServices>();
builder.Services.AddScoped<IDetailService, DetailService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddScoped<IGameStatService, GameStatService>();
builder.Services.AddScoped<IPlayerStatsService, PlayerStatsService>();
builder.Services.AddScoped<IChallangesServices, ChallangesServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
AppDbInitializer.SeedPlayer(app);
AppDbInitializer.Seed(app);
app.Run();
