
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeRS.Core.Models;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();
            string query = "SELECT * FROM Reviews";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review review = new Review(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetInt32(reader.GetOrdinal("AnimeId")),
                                reader.GetString(reader.GetOrdinal("Comment")),
                                reader.GetInt32(reader.GetOrdinal("Rating")),
                                reader.GetDateTime(reader.GetOrdinal("DatePosted"))
                            );
                            reviews.Add(review);
                        }
                    }
                }
            }

            return reviews;
        }

        public Review GetReviewById(int id)
        {
            string query = "SELECT * FROM Reviews WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Review(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetInt32(reader.GetOrdinal("AnimeId")),
                            reader.GetString(reader.GetOrdinal("Comment")),
                            reader.GetInt32(reader.GetOrdinal("Rating")),
                            reader.GetDateTime(reader.GetOrdinal("DatePosted"))
                        );
                    }
                }
            }
            return null;  // No review found with the specified ID
        }

        public void AddReview(Review review)
        {
            string query = "INSERT INTO Reviews (AnimeId, Comment, Rating, DatePosted) VALUES (@AnimeId, @Comment, @Rating, @DatePosted)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AnimeId", review.AnimeId);
                command.Parameters.AddWithValue("@Comment", review.Comment);
                command.Parameters.AddWithValue("@Rating", review.Rating);
                command.Parameters.AddWithValue("@DatePosted", review.DatePosted);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateReview(Review review)
        {
            string query = "UPDATE Reviews SET AnimeId = @AnimeId, Comment = @Comment, Rating = @Rating, DatePosted = @DatePosted WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", review.Id);
                command.Parameters.AddWithValue("@AnimeId", review.AnimeId);
                command.Parameters.AddWithValue("@Comment", review.Comment);
                command.Parameters.AddWithValue("@Rating", review.Rating);
                command.Parameters.AddWithValue("@DatePosted", review.DatePosted);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteReview(int id)
        {
            string query = "DELETE FROM Reviews WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
