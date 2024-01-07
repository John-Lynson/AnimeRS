using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Linq;
using AnimeRS.Core.ViewModels;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IAnimeService _animeService;

        public ReviewService(IReviewRepository reviewRepository, IAnimeService animeService)
        {
            _reviewRepository = reviewRepository;
            _animeService = animeService;
        }

    public IEnumerable<Review> GetAllReviews()
        {
            var reviewDTOs = _reviewRepository.GetAllReviews();
            return reviewDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
        }

        public Review GetReviewById(int id)
        {
            var reviewDTO = _reviewRepository.GetReviewById(id);
            return AnimeRSConverter.ConvertToDomain(reviewDTO);
        }

        public void AddReview(Review review)
        {
            review.DatePosted = DateTime.UtcNow; 
            var reviewDTO = AnimeRSConverter.ConvertToReviewDto(review);
            _reviewRepository.AddReview(reviewDTO);

            Console.WriteLine($"AnimeId: {review.AnimeId}");
        }

        public void UpdateReview(Review review)
        {
            var reviewDTO = AnimeRSConverter.ConvertToReviewDto(review);
            _reviewRepository.UpdateReview(reviewDTO);
        }

        public IEnumerable<Review> GetReviewsByAnimeId(int animeId)
        {
            var reviewDTOs = _reviewRepository.GetReviewsByAnimeId(animeId);
            return reviewDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
        }

        public double GetAverageScore(int animeId)
        {
            var reviewDTOs = _reviewRepository.GetReviewsByAnimeId(animeId);
            if (!reviewDTOs.Any()) return 0; 

            double totalScore = reviewDTOs.Sum(r => r.Rating) * 2; 
            double averageScore = totalScore / reviewDTOs.Count(); 

            return averageScore;
        }

        public IEnumerable<ReviewViewModel> GetReviewsByUserId(int userId)
        {
            var reviewDTOs = _reviewRepository.GetReviewsByAnimeLoverId(userId);
            var reviewViewModels = reviewDTOs.Select(dto =>
            {
                var anime = _animeService.GetAnimeById(dto.AnimeId);
                return new ReviewViewModel
                {
                    Id = dto.Id,
                    AnimeId = dto.AnimeId,
                    Comment = dto.Comment,
                    Rating = dto.Rating,
                    DatePosted = dto.DatePosted,
                    AnimeTitle = anime?.Title
                };
            });

            return reviewViewModels;
        }



        public void DeleteReview(int id)
        {
            _reviewRepository.DeleteReview(id);
        }
    }
}