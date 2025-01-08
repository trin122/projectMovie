using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangepasswordController : ControllerBase
    {
        private readonly string _connectionString;

        public ChangepasswordController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBMovie");
        }

        // POST: api/Changepassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] Changepassword request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest("Name, old password, new password, and confirm password must be provided.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest("New password and confirm password do not match.");
            }

            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn để lấy thông tin người dùng theo tên (Name)
                using (SqlCommand command = new SqlCommand("SELECT Id, Name, Password FROM users WHERE Name = @Name", connection))
                {
                    command.Parameters.AddWithValue("@Name", request.Name);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Password = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            // Kiểm tra tên người dùng và mật khẩu cũ
            if (user == null || user.Password != request.OldPassword)
            {
                return Unauthorized("Invalid name or old password.");
            }

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string updateQuery = "UPDATE users SET Password = @Password WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Password", request.NewPassword);  // Mật khẩu mới chưa mã hóa
                    command.Parameters.AddWithValue("@Id", user.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return Ok("Password changed successfully.");
        }
    }
}
