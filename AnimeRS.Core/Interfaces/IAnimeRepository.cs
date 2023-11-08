using AnimeRS.Core.Models;
using System.Collections.Generic;

namespace AnimeRS.Core.Interfaces
{
    public interface IAnimeRepository
    {
        IEnumerable<Anime> GetAllAnimes();
        Anime GetAnimeById(int id);
        void AddAnime(Anime anime); 
        void UpdateAnime(Anime anime);
        void DeleteAnime(int id); 
    }
}
