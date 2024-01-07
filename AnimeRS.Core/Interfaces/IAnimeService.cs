using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AnimeRS.Core.Models;
using AnimeRS.Core;

namespace AnimeRS.Core.Interfaces
{
    public interface IAnimeService
    {
        // Methode om een anime te verkrijgen op basis van ID
        Anime GetAnimeById(int id);

        // Andere methoden die door ReviewService of andere delen van de applicatie worden gebruikt
        // Voorbeeld: Voeg de methoden toe die je in AnimeService hebt gedefinieerd en die extern gebruikt worden
        IEnumerable<Anime> GetAllAnimes();
        void AddAnime(Anime anime);
        void UpdateAnime(Anime anime);
        IEnumerable<Anime> SearchAnimes(string query);
        IEnumerable<Anime> GetTopAnimesByTotalRating(int count);
        void DeleteAnime(int id);
    }
}
