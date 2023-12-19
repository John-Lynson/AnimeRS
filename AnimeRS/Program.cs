using AnimeRS.Data.Database;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.Repositories;
using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Voeg Auth0 configuratie toe
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddControllersWithViews();

// Haal de databaseverbindingstring op en registreer DatabaseConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DatabaseConnection(connectionString));

// Registreer repositories
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAnimeLoverRepository, AnimeLoverRepository>();

// Registreer services
builder.Services.AddScoped<AnimeService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<AnimeLoverService>();

var app = builder.Build();

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
