﻿using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Data.Database;

namespace AnimeRS.Data.Repositories
{
    public class AnimeLoverRepository : IAnimeLoverRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public AnimeLoverRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<AnimeLoverDTO> GetAllAnimeLovers()
        {
            List<AnimeLoverDTO> animeLovers = new List<AnimeLoverDTO>();
            string query = "SELECT * FROM AnimeLovers";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var animeLover = new AnimeLoverDTO
                            {
                                Username = reader["Username"].ToString(),
                                Role = reader["Role"].ToString(),
                                Auth0UserId = reader["Auth0UserId"].ToString()
                            };
                            animeLovers.Add(animeLover);
                        }
                    }
                }
            }

            return animeLovers;
        }

        public AnimeLoverDTO GetAnimeLoverById(int id)
        {
            AnimeLoverDTO animeLover = null;
            string query = "SELECT * FROM AnimeLovers WHERE Id = @Id";

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
                            animeLover = new AnimeLoverDTO
                            {
                                Username = reader["Username"].ToString(),
                                Role = reader["Role"].ToString(),
                                Auth0UserId = reader["Auth0UserId"].ToString()
                            };
                        }
                    }
                }
            }

            if (animeLover == null)
            {
                throw new KeyNotFoundException($"No AnimeLover found with ID {id}");
            }

            return animeLover;
        }


        public AnimeLoverDTO GetAnimeLoverByUsername(string username)
        {
            string query = "SELECT * FROM AnimeLovers WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new AnimeLoverDTO
                        {
                            Username = username,
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            Auth0UserId = reader.GetString(reader.GetOrdinal("Auth0UserId"))
                        };
                    }
                    return null;
                }
            }
        }

        public bool AddAnimeLover(AnimeLoverDTO animeLover)
        {
            if (animeLover == null)
            {
                throw new ArgumentNullException(nameof(animeLover));
            }

            string query = @"
              INSERT INTO AnimeLovers (Username, Role, Auth0UserId)
              VALUES (@Username, @Role, @Auth0UserId)";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Role", animeLover.Role);
                    command.Parameters.AddWithValue("@Auth0UserId", animeLover.Auth0UserId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool UpdateAnimeLover(AnimeLoverDTO animeLover)
        {
            if (animeLover == null)
            {
                throw new ArgumentNullException(nameof(animeLover));
            }


            string query = @"
        UPDATE AnimeLovers
        SET Username = @Username, Role = @Role
        WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", animeLover.Id);
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Role", animeLover.Role);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public AnimeLoverDTO GetByAuth0UserId(string auth0UserId)
        {


            AnimeLoverDTO animeLover = null;
            string query = "SELECT * FROM AnimeLovers WHERE Auth0UserId = @Auth0UserId";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Auth0UserId", auth0UserId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animeLover = new AnimeLoverDTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")), // Voeg deze regel toe
                                Username = reader["Username"].ToString(),
                                Role = reader["Role"].ToString(),
                                Auth0UserId = auth0UserId
                            };
                        }
                    }
                }
            }

            return animeLover;
        }


        public bool DeleteAnimeLover(int id)
        {
            string query = "DELETE FROM AnimeLovers WHERE Id = @Id";

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