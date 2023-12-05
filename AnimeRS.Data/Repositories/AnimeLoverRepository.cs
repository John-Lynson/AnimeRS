using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnimeRS.Data.Repositories
{
    public class AnimeLoverRepository : IAnimeLoverRepository
    {
        private readonly string _connectionString;

        public AnimeLoverRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<AnimeLover> GetAllAnimeLovers()
        {
            List<AnimeLover> animeLovers = new List<AnimeLover>();
            string query = "SELECT * FROM AnimeLovers";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var animeLover = new AnimeLover(
                                reader["Username"].ToString(),
                                reader["Role"].ToString(),
                                reader["Auth0UserId"].ToString()
                            );
                            animeLovers.Add(animeLover);
                        }
                    }
                }
            }

            return animeLovers;
        }



        public AnimeLover GetAnimeLoverById(int id)
        {
            AnimeLover animeLover = null;
            string query = "SELECT * FROM AnimeLovers WHERE Id = @Id";

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
                            animeLover = new AnimeLover(
                                reader["Username"].ToString(),
                                reader["Role"].ToString(),
                                reader["Auth0UserId"].ToString()
                            );
                        }
                    }
                }
            }

            return animeLover;
        }




        public AnimeLover GetAnimeLoverByUsername(string username)
        {
            string query = "SELECT * FROM AnimeLovers WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var role = reader.GetString(reader.GetOrdinal("Role"));
                        var auth0UserId = reader.GetString(reader.GetOrdinal("Auth0UserId"));

                        return new AnimeLover(username, role, auth0UserId);
                    }
                    return null;
                }
            }
        }



        public bool AddAnimeLover(AnimeLover animeLover)
        {
            string query = @"
        INSERT INTO AnimeLovers (Username, Role)
        VALUES (@Username, @Role)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Role", animeLover.Role);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool UpdateAnimeLover(AnimeLover animeLover)
        {
            string query = @"
        UPDATE AnimeLovers
        SET Username = @Username, Role = @Role
        WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public AnimeLover GetByAuth0UserId(string auth0UserId)
        {
            AnimeLover animeLover = null;
            string query = "SELECT * FROM AnimeLovers WHERE Auth0UserId = @Auth0UserId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Auth0UserId", auth0UserId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animeLover = new AnimeLover(
                                reader["Username"].ToString(),
                                reader["Role"].ToString(),
                                auth0UserId
                            );
                        }
                    }
                }
            }

            return animeLover;
        }


        public bool DeleteAnimeLover(int id)
        {
            string query = "DELETE FROM AnimeLovers WHERE Id = @Id";
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


