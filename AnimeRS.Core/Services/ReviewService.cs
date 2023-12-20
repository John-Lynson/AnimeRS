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
            var reviewDTO = AnimeRSConverter.ConvertToDto(review);
            _reviewRepository.AddReview(reviewDTO);
        }

        public void UpdateReview(Review review)
        {
            var reviewDTO = AnimeRSConverter.ConvertToDto(review);
            _reviewRepository.UpdateReview(reviewDTO);
        }

        public void DeleteReview(int id)
        {
            _reviewRepository.DeleteReview(id);
        }
    }
}
