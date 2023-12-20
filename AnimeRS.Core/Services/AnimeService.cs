using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnimeRS.Core.Services
{
    public class AnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public IEnumerable<Anime> GetAllAnimes()
        {
            var animeDTOs = _animeRepository.GetAllAnimes();
            return animeDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
        }

        public Anime GetAnimeById(int id)
        {
            var animeDTO = _animeRepository.GetAnimeById(id);
            return AnimeRSConverter.ConvertToDomain(animeDTO);
        }

        public void AddAnime(Anime anime)
        {
            var animeDTO = AnimeRSConverter.ConvertToDto(anime);
            _animeRepository.AddAnime(animeDTO);
        }

        public void UpdateAnime(Anime anime)
        {
            var animeDTO = AnimeRSConverter.ConvertToDto(anime);
            _animeRepository.UpdateAnime(animeDTO);
        }

        public void DeleteAnime(int id)
        {
            _animeRepository.DeleteAnime(id);
        }
    }
}
