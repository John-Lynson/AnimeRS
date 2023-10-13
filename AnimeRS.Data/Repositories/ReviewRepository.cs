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
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            var reviews = new List<Review>();
            var query = "SELECT * FROM Reviews";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var review = new Review
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                                Comment = reader.GetString(reader.GetOrdinal("Comment")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                DatePosted = reader.GetDateTime(reader.GetOrdinal("DatePosted"))
                            };
                            reviews.Add(review);
                        }
                    }
                }
            }

            return reviews;
        }

        public Review GetReviewById(int id)
        {
            var query = "SELECT * FROM Reviews WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Review
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                            DatePosted = reader.GetDateTime(reader.GetOrdinal("DatePosted"))
                        };
                    }
                }
            }
            return null;  // Geen review gevonden met de opgegeven ID
        }

        public void AddReview(Review review)
        {
            var query = "INSERT INTO Reviews (AnimeId, Comment, Rating, DatePosted) VALUES (@AnimeId, @Comment, @Rating, @DatePosted)";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
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
            var query = "UPDATE Reviews SET AnimeId = @AnimeId, Comment = @Comment, Rating = @Rating, DatePosted = @DatePosted WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
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
            var query = "DELETE FROM Reviews WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

