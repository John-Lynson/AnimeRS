using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AnimeRS.Data.Repositories
{
    internal class FavoriteAnimeRepository : IFavoriteAnimeRepository
    {
        private readonly string _connectionString;

        public FavoriteAnimeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<FavoriteAnime>> GetFavoriteAnimesByAnimeLoverIdAsync(int animeLoverId)
        {
            var favoriteAnimes = new List<FavoriteAnime>();
            var query = "SELECT * FROM FavoriteAnimes WHERE AnimeLoverId = @AnimeLoverId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeLoverId", animeLoverId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var favoriteAnime = new FavoriteAnime(
                                reader.GetInt32(reader.GetOrdinal("AnimeLoverId")),
                                reader.GetInt32(reader.GetOrdinal("AnimeId"))
                            );
                            favoriteAnimes.Add(favoriteAnime);
                        }
                    }
                }
            }

            return favoriteAnimes;
        }

        public async Task<bool> AddFavoriteAnimeAsync(FavoriteAnime favoriteAnime)
        {
            var query = @"
                INSERT INTO FavoriteAnimes (AnimeLoverId, AnimeId)
                VALUES (@AnimeLoverId, @AnimeId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeLoverId", favoriteAnime.AnimeLoverId);
                    command.Parameters.AddWithValue("@AnimeId", favoriteAnime.AnimeId);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> RemoveFavoriteAnimeAsync(int id)
        {
            var query = "DELETE FROM FavoriteAnimes WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
