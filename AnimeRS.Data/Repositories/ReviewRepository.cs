using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Data.Database;

namespace AnimeRS.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public ReviewRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<ReviewDTO> GetAllReviews()
        {
            var reviews = new List<ReviewDTO>();
            string query = "SELECT * FROM Reviews";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var review = new ReviewDTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                                AnimeLoverId = reader.GetInt32(reader.GetOrdinal("AnimeLoverId")),
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

        public ReviewDTO GetReviewById(int id)
        {
            string query = "SELECT * FROM Reviews WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ReviewDTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                            AnimeLoverId = reader.GetInt32(reader.GetOrdinal("AnimeLoverId")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                            DatePosted = reader.GetDateTime(reader.GetOrdinal("DatePosted"))
                        };
                    }
                }
            }
            return null;  // No review found with the specified ID
        }

        public void AddReview(ReviewDTO review)
        {
            string query = "INSERT INTO Reviews (AnimeId, AnimeLoverId, Comment, Rating, DatePosted) VALUES (@AnimeId, @AnimeLoverId, @Comment, @Rating, @DatePosted)";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AnimeId", review.AnimeId);
                command.Parameters.AddWithValue("@AnimeLoverId", review.AnimeLoverId);
                command.Parameters.AddWithValue("@Comment", review.Comment);
                command.Parameters.AddWithValue("@Rating", review.Rating);
                command.Parameters.AddWithValue("@DatePosted", review.DatePosted);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateReview(ReviewDTO review)
        {
            string query = "UPDATE Reviews SET AnimeId = @AnimeId, Comment = @Comment, Rating = @Rating, DatePosted = @DatePosted WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
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

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
