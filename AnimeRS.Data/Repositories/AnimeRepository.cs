using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            var animes = new List<Anime>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Animes"))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var anime = MapToAnime(reader);
                            animes.Add(anime);
                        }
                    }
                }
            }

            return animes;
        }

        public async Task<Anime> GetAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Animes WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToAnime(reader);
                        }
                    }
                }
            }

            return null;

        }

        public async Task AddAsync(Anime anime)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(
                    "INSERT INTO Animes (Title, Description, Genre, ReleaseDate) VALUES (@Title, @Description, @Genre, @ReleaseDate",
                    connection))
                {
                    command.Parameters.AddWithValue("@Title", anime.Title);
                    command.Parameters.AddWithValue("@Description", anime.Description);
                    command.Parameters.AddWithValue("@Genre", anime.Genre);
                    command.Parameters.AddWithValue("@ReleaseData", anime.ReleaseDate);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private Anime MapToAnime(SqlDataReader reader)
        {
            return new Anime(
                id: reader.GetInt32(reader.GetOrdinal("Id")),
                title: reader.GetString(reader.GetOrdinal("Title")),
                description: reader.GetString(reader.GetOrdinal("Description")),
                genre: reader.GetString(reader.GetOrdinal("Genre")),
                releaseDate: reader.GetDateTime(reader.GetOrdinal("ReleaseDate"))
            );
        }

    }
}
