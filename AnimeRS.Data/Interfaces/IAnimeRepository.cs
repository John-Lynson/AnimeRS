using AnimeRS.Data.dto;
using System.Collections.Generic;

namespace AnimeRS.Data.Interfaces
{
    public interface IAnimeRepository
    {
        IEnumerable<AnimeDTO> GetAllAnimes();

        IEnumerable<AnimeDTO> SearchAnimes(string query);
        AnimeDTO GetAnimeById(int id);
        void AddAnime(AnimeDTO anime);
        void UpdateAnime(AnimeDTO anime);
        void DeleteAnime(int id);
    }
}
