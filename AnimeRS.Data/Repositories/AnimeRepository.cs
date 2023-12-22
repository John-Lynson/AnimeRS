using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Data.Database;

namespace AnimeRS.Data.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public AnimeRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<AnimeDTO> GetAllAnimes()
        {
            var animes = new List<AnimeDTO>();
            string query = "SELECT * FROM Animes";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var anime = new AnimeDTO
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Genre = reader["Genre"].ToString(),
                                Episodes = reader.GetInt32(reader.GetOrdinal("Episodes")),
                                Status = reader["Status"].ToString(),
                                ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString()),
                                ImageURL = reader["Image_url"] == DBNull.Value ? null : reader["Image_url"].ToString()
                            };
                            animes.Add(anime);
                        }
                    }
                }
            }

            return animes;
        }

        public AnimeDTO GetAnimeById(int id)
        {
            AnimeDTO anime = null;
            string query = "SELECT * FROM Animes WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            anime = new AnimeDTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Genre = reader.GetString(reader.GetOrdinal("Genre")),
                                Episodes = reader.GetInt32(reader.GetOrdinal("Episodes")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString()),
                                ImageURL = reader["Image_url"] == DBNull.Value ? null : reader["Image_url"].ToString()
                            };
                        }
                    }
                }
            }

            return anime;
        }

        public void AddAnime(AnimeDTO anime)
        {
            string query = @"INSERT INTO Animes (Title, Description, Genre, Episodes, Status, ReleaseDate, image_url)
                     VALUES (@Title, @Description, @Genre, @Episodes, @Status, @ReleaseDate, @ImageURL)";
            using (var connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", anime.Title);
                    command.Parameters.AddWithValue("@Description", anime.Description);
                    command.Parameters.AddWithValue("@Genre", anime.Genre);
                    command.Parameters.AddWithValue("@Episodes", anime.Episodes);
                    command.Parameters.AddWithValue("@Status", anime.Status);
                    command.Parameters.AddWithValue("@ReleaseDate", anime.ReleaseDate);
                    command.Parameters.AddWithValue("@ImageURL", anime.ImageURL ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAnime(AnimeDTO anime)
        {
            string query = @"
UPDATE Animes
SET Title = @Title, Description = @Description, Genre = @Genre,
    Episodes = @Episodes, Status = @Status, ReleaseDate = @ReleaseDate, Image_url = @ImageURL
WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", anime.Id);
                        command.Parameters.AddWithValue("@Title", anime.Title);
                        command.Parameters.AddWithValue("@Description", anime.Description);
                        command.Parameters.AddWithValue("@Genre", anime.Genre);
                        command.Parameters.AddWithValue("@Episodes", anime.Episodes);
                        command.Parameters.AddWithValue("@Status", anime.Status);
                        command.Parameters.AddWithValue("@ReleaseDate", anime.ReleaseDate);
                        command.Parameters.AddWithValue("@ImageURL", anime.ImageURL ?? (object)DBNull.Value); // Zorg voor consistentie in parameter naam
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Overweeg een meer specifieke foutafhandeling
            }
        }

        public IEnumerable<AnimeDTO> SearchAnimes(string title, string genre)
        {
            var animes = new List<AnimeDTO>();
            string query = "SELECT * FROM Animes WHERE (@Title IS NULL OR Title LIKE @Title) AND (@Genre IS NULL OR Genre LIKE @Genre)";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", string.IsNullOrEmpty(title) ? (object)DBNull.Value : $"%{title}%");
                    command.Parameters.AddWithValue("@Genre", string.IsNullOrEmpty(genre) ? (object)DBNull.Value : $"%{genre}%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var anime = new AnimeDTO
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Genre = reader["Genre"].ToString(),
                                Episodes = reader.GetInt32(reader.GetOrdinal("Episodes")),
                                Status = reader["Status"].ToString(),
                                ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString()),
                                ImageURL = reader["Image_url"] == DBNull.Value ? null : reader["Image_url"].ToString()
                            };
                            animes.Add(anime);
                        }
                    }
                }
            }

            return animes;
        }



        public void DeleteAnime(int id)
        {
            string query = "DELETE FROM Animes WHERE Id = @Id";

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