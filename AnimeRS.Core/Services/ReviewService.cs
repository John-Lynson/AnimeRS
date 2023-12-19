using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System.Collections.Generic;

namespace AnimeRS.Core.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<ReviewDTO> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        public ReviewDTO GetReviewById(int id)
        {
            return _reviewRepository.GetReviewById(id);
        }

        public void AddReview(ReviewDTO reviewDTO)
        {
            _reviewRepository.AddReview(reviewDTO);
        }

        public void UpdateReview(ReviewDTO reviewDTO)
        {
            _reviewRepository.UpdateReview(reviewDTO);
        }

        public void DeleteReview(int id)
        {
            _reviewRepository.DeleteReview(id);
        }
    }
}
