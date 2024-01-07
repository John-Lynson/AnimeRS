using AnimeRS.Core.Models;
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
    }
}
