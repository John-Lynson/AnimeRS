using AnimeRS.Data.dto;
using System.Xml.Linq;

namespace AnimeRS.Data.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<ReviewDTO> GetAllReviews();
        ReviewDTO GetReviewById(int id);
        IEnumerable<ReviewDTO> GetReviewsByAnimeId(int animeId);
        IEnumerable<ReviewDTO> GetReviewsByAnimeLoverId(int animeLoverId);
        void AddReview(ReviewDTO review);
        void UpdateReview(ReviewDTO review);
        void DeleteReview(int id);
    }
}
