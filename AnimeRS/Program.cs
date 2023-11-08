using AnimeRS.Core.Models;
using AnimeRS.Core.Interfaces;
using AnimeRS.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Verander deze regel om MVC te ondersteunen in plaats van Razor Pages
builder.Services.AddControllersWithViews();

// Configureer de DatabaseSettings om de ConnectionStrings sectie uit het configuratiebestand te gebruiken
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Registreer de IAnimeRepository en zijn implementatie AnimeRepository, 
// en zorg ervoor dat de AnimeRepository de DatabaseSettings krijgt die uit de configuratie zijn geladen.
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

var app = builder.Build();

// Configureer de HTTP-verzoekpipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Verwijs naar een error-actie in je HomeController
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();