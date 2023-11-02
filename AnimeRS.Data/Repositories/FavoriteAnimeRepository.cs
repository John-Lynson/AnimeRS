
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnimeRS.Data.Repositories
{
    internal class FavoriteAnimeRepository : IFavoriteAnimeRepository
    {
        private readonly string _connectionString;

        public FavoriteAnimeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<FavoriteAnime> GetFavoriteAnimesByAnimeLoverId(int animeLoverId)
        {
            List<FavoriteAnime> favoriteAnimes = new List<FavoriteAnime>();
            string query = "SELECT * FROM FavoriteAnimes WHERE AnimeLoverId = @AnimeLoverId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeLoverId", animeLoverId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FavoriteAnime favoriteAnime = new FavoriteAnime(
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

        public bool AddFavoriteAnime(FavoriteAnime favoriteAnime)
        {
            string query = @"
                INSERT INTO FavoriteAnimes (AnimeLoverId, AnimeId)
                VALUES (@AnimeLoverId, @AnimeId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeLoverId", favoriteAnime.AnimeLoverId);
                    command.Parameters.AddWithValue("@AnimeId", favoriteAnime.AnimeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool RemoveFavoriteAnime(int id)
        {
            string query = "DELETE FROM FavoriteAnimes WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
