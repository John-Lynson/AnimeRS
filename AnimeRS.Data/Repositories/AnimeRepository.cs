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
                using (var command = connection.CreateCommand("SELECT * FROM Animes")) 
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

        public async Task<Anime> GetAsync (int id)
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
    }
}
