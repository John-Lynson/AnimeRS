using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using AnimeRS.Data.Database;
using AnimeRS.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options; // Zorg ervoor dat je deze using toevoegt

var builder = WebApplication.CreateBuilder(args);

// Verander deze regel om MVC te ondersteunen in plaats van Razor Pages
builder.Services.AddControllersWithViews();

// Configureer de DatabaseSettings om de ConnectionStrings sectie uit het configuratiebestand te gebruiken
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Registreer de IAnimeRepository en zijn implementatie AnimeRepository, 
// en zorg ervoor dat de AnimeRepository de DatabaseSettings krijgt die uit de configuratie zijn geladen.
// Dit gebruikt een factory om de AnimeRepository te creëren met de IOptions wrapper.
builder.Services.AddScoped<IAnimeRepository>(serviceProvider =>
{
    var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new AnimeRepository(databaseSettings.DefaultConnection); // Pass the connection string, not the settings object
});

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
