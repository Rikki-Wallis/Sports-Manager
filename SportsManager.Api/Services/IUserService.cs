using SportsManager.Api.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SportsManager.Api.Services
{
    public interface IUserService
    {
        Task<RegistrationResult> RegisterUserAsync(UserRegistrationDto dto);
    }

    public class UserService : IUserService 
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<RegistrationResult> RegisterUserAsync(UserRegistrationDto dto)
        {
            try
            {
                // Encrypt password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                // Get connection to db
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Add command
                    var command = new SqlCommand(
                        "INSERT INTO Users (user_Email, user_Username, user_PasswordHash, user_CreatedDate)" +
                        "OUTPUT INSERTED.UserId" +
                        "VALUES (@Email, @Username, @PasswordHash, @CreatedDate)",
                        connection
                        );

                    // Set parameters
                    command.Parameters.AddWithValue("@Email", dto.EmailAddress);
                    command.Parameters.AddWithValue("@Username", dto.Username);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);

                    // Fetch user id
                    var userId = (int)await command.ExecuteScalarAsync();

                    return new RegistrationResult { Success = true, UserId = userId };

                }
            }
            catch (SqlException ex)
            {
                // Handle duplication errors
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return new RegistrationResult { Success = false, ErrorMessage = "Email or username is already in use" };
                }

                return new RegistrationResult { Success = false, ErrorMessage = "Database error occurred"};

            }
        }
    }
}
