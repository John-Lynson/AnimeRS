using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using AnimeRS.Data.Database;
using AnimeRS.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Voeg Auth0 configuratie toe
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});


builder.Services.AddControllersWithViews();

// Configureer de DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Registreer uw repositories en services
builder.Services.AddScoped<IAnimeLoverRepository, AnimeLoverRepository>(serviceProvider =>
{
    var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new AnimeLoverRepository(databaseSettings.DefaultConnection);
});

builder.Services.AddScoped<AnimeLoverService>();
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>(serviceProvider =>
{
    var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new AnimeRepository(databaseSettings.DefaultConnection);
});

// Voeg autorisatie toe
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

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
