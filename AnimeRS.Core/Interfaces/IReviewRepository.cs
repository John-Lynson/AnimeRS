using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeRS.Core.Models;

namespace AnimeRS.Core.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviews();
        Review GetReviewById(int id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int id);
    }
}
