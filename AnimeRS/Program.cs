using AnimeRS.Data.Database;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.Repositories;
using AnimeRS.Core.Services;
using AnimeRS.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Auth0 configuratie
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// Controllers met Views (of MVC)
builder.Services.AddControllersWithViews();

// Database verbinding
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DatabaseConnection(connectionString));

// Repository registraties
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAnimeLoverRepository, AnimeLoverRepository>();
builder.Services.AddScoped<IFavoriteAnimeRepository, FavoriteAnimeRepository>();

// Service registraties
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAnimeLoverService, AnimeLoverService>();
builder.Services.AddScoped<IFavoriteAnimeService, FavoriteAnimeService>();

var app = builder.Build();

// Configureer de HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
