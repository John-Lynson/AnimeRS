using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Linq;
using AnimeRS.Data.Repositories;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.Core.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IReviewRepository _reviewRepository;

        public AnimeService(IAnimeRepository animeRepository, IReviewRepository reviewRepository)
        {
            _animeRepository = animeRepository;
            _reviewRepository = reviewRepository;
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

        public IEnumerable<Anime> SearchAnimes(string query)
        {
            var animeDTOs = _animeRepository.SearchAnimes(query);
            return animeDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
        }

        public IEnumerable<Anime> GetTopAnimesByTotalRating(int count = 3)
        {

            var allReviews = _reviewRepository.GetAllReviews();

            var totalRatings = allReviews
                .GroupBy(r => r.AnimeId)
                .Select(g => new
                {
                    AnimeId = g.Key,
                    TotalRating = g.Sum(r => r.Rating)
                })
                .OrderByDescending(g => g.TotalRating)
                .Take(count);

            var topAnimesDTOs = totalRatings.Select(rating => _animeRepository.GetAnimeById(rating.AnimeId));
            var topAnimes = topAnimesDTOs.Select(dto => AnimeRSConverter.ConvertToDomain(dto));

            return topAnimes;
        }

        public void DeleteAnime(int id)
        {
            _animeRepository.DeleteAnime(id);
        }
    }
}
