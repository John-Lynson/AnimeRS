using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;

namespace AnimeRS.Data.Repositories
{
    internal class AnimeRepository : IAnimeRepository
    {
        private readonly string _connectionString;

        public AnimeRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<Anime>> GetAllAnimesAsync()
        {
            var animes = new List<Anime>();
            var query = "SELECT * FROM Animes";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public async Task<Anime> GetAnimeByIdAsync(int id)
        {
            Anime anime = null;
            var query = "SELECT * FROM Animes WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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
                        }
                    }
                }
            }

            return anime;
        }


        public async Task AddAnimeAsync(Anime anime)
        {
            var query = @"
                INSERT INTO Animes (Title, Description, Genre, Episodes, Status, ReleaseDate)
                VALUES (@Title, @Description, @Genre, @Episodes, @Status, @ReleaseDate)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", anime.Title);
                    command.Parameters.AddWithValue("@Description", anime.Description);
                    command.Parameters.AddWithValue("@Genre", anime.Genre);
                    command.Parameters.AddWithValue("@Episodes", anime.Episodes);
                    command.Parameters.AddWithValue("@Status", anime.Status);
                    command.Parameters.AddWithValue("@ReleaseDate", anime.ReleaseDate);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAnimeAsync(Anime anime)
        {
            var query = @"
                UPDATE Animes
                SET Title = @Title, Description = @Description, Genre = @Genre,
                    Episodes = @Episodes, Status = @Status, AiringDate = @AiringDate
                WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", anime.Id);
                    command.Parameters.AddWithValue("@Title", anime.Title);
                    command.Parameters.AddWithValue("@Description", anime.Description);
                    command.Parameters.AddWithValue("@Genre", anime.Genre);
                    command.Parameters.AddWithValue("@Episodes", anime.Episodes);
                    command.Parameters.AddWithValue("@Status", anime.Status);
                    command.Parameters.AddWithValue("@ReleaseDate", anime.ReleaseDate);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAnimeAsync(int id)
        {
            var query = "DELETE FROM Animes WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
