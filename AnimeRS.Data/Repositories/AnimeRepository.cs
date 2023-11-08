using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;

namespace AnimeRS.Data.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly string _connectionString;

        public AnimeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Anime> GetAllAnimes()
        {
            var animes = new List<Anime>();
            string query = "SELECT * FROM Animes";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var anime = new Anime(
                                Convert.ToInt32(reader["Id"].ToString()),
                                reader["Title"].ToString(),
                                reader["Description"].ToString(),
                                reader["Genre"].ToString(),
                                reader.GetInt32(reader.GetOrdinal("Episodes")),
                                reader["Status"].ToString(),
                                DateTime.Parse(reader["ReleaseDate"].ToString())
                            );
                            animes.Add(anime);
                        }
                    }
                }
            }

            return animes;
        }


        public Anime GetAnimeById(int id)
        {
            Anime anime = null;
            string query = "SELECT * FROM Animes WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            anime = new Anime(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("Description")),
                                reader.GetString(reader.GetOrdinal("Genre")),
                                reader.GetInt32(reader.GetOrdinal("Episodes")),
                                reader.GetString(reader.GetOrdinal("Status")),
                                DateTime.Parse(reader["ReleaseDate"].ToString())  // Deze regel is aangepast
                            );
                            return default;
                        }
                    }
                }
            }

            return anime;
        }


        public void AddAnime(Anime anime)
        {
            string query = @"INSERT INTO Animes (Title, Description, Genre, Episodes, Status, ReleaseDate)
                     VALUES (@Title, @Description, @Genre, @Episodes, @Status, @ReleaseDate)";
            using (var connection = new SqlConnection(_connectionString))
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

        public void UpdateAnime(Anime anime)
        {
            string query = @"
        UPDATE Animes
        SET Title = @Title, Description = @Description, Genre = @Genre,
            Episodes = @Episodes, Status = @Status, AiringDate = @AiringDate
        WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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


        public void DeleteAnime(int id)
        {
            string query = "DELETE FROM Animes WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
