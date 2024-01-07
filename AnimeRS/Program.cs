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

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DatabaseConnection(connectionString));

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAnimeLoverRepository, AnimeLoverRepository>();
builder.Services.AddScoped<IFavoriteAnimeRepository, FavoriteAnimeRepository>();

// Registreer services
builder.Services.AddScoped<AnimeService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<AnimeLoverService>();
builder.Services.AddScoped<FavoriteAnimeService>();

builder.Services.AddScoped<IFavoriteAnimeService>();
builder.Services.AddScoped<IReviewService>();
builder.Services.AddScoped<IAnimeLoverService>();
builder.Services.AddScoped<IFavoriteAnimeService>();


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
