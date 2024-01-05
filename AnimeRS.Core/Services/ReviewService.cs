using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnimeRS.Core.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
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
            review.DatePosted = DateTime.UtcNow; // Stel de huidige UTC tijd in
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

        public void DeleteReview(int id)
        {
            _reviewRepository.DeleteReview(id);
        }
    }
}
