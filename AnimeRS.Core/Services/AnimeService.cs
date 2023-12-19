using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System.Collections.Generic;

namespace AnimeRS.Core.Services
{
    public class AnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public IEnumerable<AnimeDTO> GetAllAnimes()
        {
            return _animeRepository.GetAllAnimes();
        }

        public AnimeDTO GetAnimeById(int id)
        {
            return _animeRepository.GetAnimeById(id);
        }

        public void AddAnime(AnimeDTO animeDTO)
        {
            _animeRepository.AddAnime(animeDTO);
        }

        public void UpdateAnime(AnimeDTO animeDTO)
        {
            _animeRepository.UpdateAnime(animeDTO);
        }

        public void DeleteAnime(int id)
        {
            _animeRepository.DeleteAnime(id);
        }
    }
}
