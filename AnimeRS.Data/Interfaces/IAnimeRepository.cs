using AnimeRS.Data.dto;
using System.Collections.Generic;

namespace AnimeRS.Data.Interfaces
{
    public interface IAnimeRepository
    {
        IEnumerable<AnimeDTO> GetAllAnimes();
        AnimeDTO GetAnimeById(int id);
        IEnumerable<AnimeDTO> GetAnimesByIds(IEnumerable<int> animeIds);
        IEnumerable<AnimeDTO> SearchAnimes(string query);
        void AddAnime(AnimeDTO anime);
        void UpdateAnime(AnimeDTO anime);
        void DeleteAnime(int id);
    }
}

