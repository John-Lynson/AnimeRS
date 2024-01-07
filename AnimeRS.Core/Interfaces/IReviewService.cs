using AnimeRS.Core.Models;
using AnimeRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Interfaces
{
    public interface IReviewService
    {
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int id);
        Review GetReviewById(int id);
        IEnumerable<Review> GetAllReviews();
        IEnumerable<ReviewViewModel> GetReviewsByUserId(int userId);
        double GetAverageScore(int animeId);
        IEnumerable<Review> GetReviewsByAnimeId(int animeId);
    }
}
