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
                                ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString())
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
                                ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString())
                            };
                        }
                    }
                }
            }

            return anime;
        }

        public void AddAnime(AnimeDTO anime)
        {
            string query = @"INSERT INTO Animes (Title, Description, Genre, Episodes, Status, ReleaseDate)
                     VALUES (@Title, @Description, @Genre, @Episodes, @Status, @ReleaseDate)";
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
        Episodes = @Episodes, Status = @Status, ReleaseDate = @ReleaseDate
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
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Overweeg een meer specifieke foutafhandeling
            }
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