using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AnimeRS.Core.Models;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.Data.Repositories
{
    internal class ReviewRepository : IReviewRepository
    {
        private readonly string connectionString;

        public ReviewRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IEnumerable<Review> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public Review GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddReview(Review review)
        {
            throw new NotImplementedException();
        }
        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
        public void DeleteReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
