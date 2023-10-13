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
    internal class AnimeLoverRepository : IAnimeLoverRepository
    {
        private readonly string _connectionString;

        public AnimeLoverRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<AnimeLover>> GetAllAnimeLoversAsync()
        {
            var animeLovers = new List<AnimeLover>();
            var query = "SELECT * FROM AnimeLovers";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var animeLover = new AnimeLover(
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



        public async Task<AnimeLover> GetAnimeLoverByIdAsync(int id)
        {
            AnimeLover animeLover = null!;
            var query = "SELECT * FROM AnimeLovers WHERE Id = @Id";

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



        public async Task<AnimeLover> GetAnimeLoverByUsernameAsync(string username)
        {
            var query = "SELECT * FROM AnimeLovers WHERE Username = @Username";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
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


        public async Task<bool> AddAnimeLoverAsync(AnimeLover animeLover)
        {
            var query = @"
        INSERT INTO AnimeLovers (Username, Email, PasswordHash, Role)
        VALUES (@Username, @Email, @PasswordHash, @Role)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Email", animeLover.Email);
                    command.Parameters.AddWithValue("@PasswordHash", animeLover.PasswordHash);
                    command.Parameters.AddWithValue("@Role", animeLover.Role);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }


        public async Task<bool> UpdateAnimeLoverAsync(AnimeLover animeLover)
        {
            var query = @"
        UPDATE AnimeLovers
        SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role
        WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", animeLover.Id);
                    command.Parameters.AddWithValue("@Username", animeLover.Username);
                    command.Parameters.AddWithValue("@Email", animeLover.Email);
                    command.Parameters.AddWithValue("@PasswordHash", animeLover.PasswordHash);
                    command.Parameters.AddWithValue("@Role", animeLover.Role);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }


        public async Task<bool> DeleteAnimeLoverAsync(int id)
        {
            var query = "DELETE FROM AnimeLovers WHERE Id = @Id";
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

