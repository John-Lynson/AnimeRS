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
        Anime GetAnimeById(int id);
        IEnumerable<Anime> GetAllAnimes();
        void AddAnime(Anime anime);
        void UpdateAnime(Anime anime);
        IEnumerable<Anime> SearchAnimes(string query);
        IEnumerable<Anime> GetTopAnimesByTotalRating(int count);
        void DeleteAnime(int id);
    }
}
