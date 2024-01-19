using AnimeRS.Data.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeRS.Data.dto;
using AnimeRS.Data.Database; 

namespace AnimeRS.Data.Repositories
{
    public class FavoriteAnimeRepository : IFavoriteAnimeRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public FavoriteAnimeRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<FavoriteAnimeDTO> GetFavoriteAnimesByAnimeLoverId(int animeLoverId)
        {
            var favoriteAnimes = new List<FavoriteAnimeDTO>();
            string query = "SELECT * FROM FavoriteAnimes WHERE AnimeLoverId = @AnimeLoverId";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeLoverId", animeLoverId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var favoriteAnime = new FavoriteAnimeDTO
                            {
                                AnimeLoverId = reader.GetInt32(reader.GetOrdinal("AnimeLoverId")),
                                AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId"))
                            };
                            favoriteAnimes.Add(favoriteAnime);
                        }
                    }
                }
            }
            return favoriteAnimes;
        }

        public bool AddFavoriteAnime(FavoriteAnimeDTO favoriteAnime)
        {
            // Eerst controleren of het record al bestaat
            var existingFavorite = GetFavoriteAnimesByAnimeLoverId(favoriteAnime.AnimeLoverId)
                .FirstOrDefault(fa => fa.AnimeId == favoriteAnime.AnimeId);

            if (existingFavorite != null)
            {
                // Record bestaat al, dus voeg niets toe
                return false;
            }

            // Voeg het nieuwe favoriete record toe
            string query = @"
        INSERT INTO FavoriteAnimes (AnimeLoverId, AnimeId)
        VALUES (@AnimeLoverId, @AnimeId)";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
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

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
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
