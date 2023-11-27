using AnimeRS.Core.Models;
using AnimeRS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            AnimeLover animeLover = new AnimeLover(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader["Username"]?.ToString(),
                                reader["Email"].ToString(),
                                reader["PasswordHash"].ToString(),
                                reader["Role"].ToString()
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
            AnimeLover animeLover = null!;
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
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader["Username"]?.ToString(),
                                reader["Email"].ToString(),
                                reader["PasswordHash"].ToString(),
                                reader["Role"].ToString()
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
                        var id = reader.GetInt32(reader.GetOrdinal("Id"));
                        var email = reader.GetString(reader.GetOrdinal("Email"));
                        var passwordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                        var role = reader.GetString(reader.GetOrdinal("Role"));
                        return new AnimeLover(id, username, email, passwordHash, role);
                    }
                    return null;
                }
            }
        }


        public bool AddAnimeLover(AnimeLover animeLover)
        {
            string query = @"
        INSERT INTO AnimeLovers (Username, Email, PasswordHash, Role)
        VALUES (@Username, @Email, @PasswordHash, @Role)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Email", animeLover.Email);
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
        SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role
        WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", animeLover.Id);
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Email", animeLover.Email);
                    command.Parameters.AddWithValue("@Role", animeLover.Role);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public AnimeLover GetByAuth0UserId(string auth0UserId)
        {
            string query = "SELECT * FROM AnimeLovers WHERE Auth0UserId = @Auth0UserId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Auth0UserId", auth0UserId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var id = reader.GetInt32(reader.GetOrdinal("Id"));
                        var username = reader.GetString(reader.GetOrdinal("Username"));
                        var email = reader.GetString(reader.GetOrdinal("Email"));
                        var role = reader.GetString(reader.GetOrdinal("Role"));
                        return new AnimeLover(id, username, email, role, auth0UserId);
                    }
                    return null;
                }
            }
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

