using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly string _connectionString;

        public RegisterController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBMovie");
        }

        // POST: api/Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                return BadRequest("All fields are required.");
            }

            // Kiểm tra định dạng email
            if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest("Invalid email format.");
            }

            // Kiểm tra xác nhận mật khẩu
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Kiểm tra xem email đã tồn tại chưa
                string checkEmailQuery = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                using (SqlCommand checkCommand = new SqlCommand(checkEmailQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Email", model.Email);
                    int count = (int)await checkCommand.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        return Conflict("Email is already registered.");
                    }
                }

                // Lưu người dùng mới vào cơ sở dữ liệu
                string insertQuery = "INSERT INTO users (Email, Password, Name) OUTPUT INSERTED.Id VALUES (@Email, @Password, @Name)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password); // Lưu mật khẩu (chưa mã hóa)
                    command.Parameters.AddWithValue("@Name", model.Name);

                    int userId = (int)await command.ExecuteScalarAsync();
                    return CreatedAtAction(nameof(GetUserById), new { id = userId }, new { userId, model.Name, model.Email });
                }
            }
        }

        // GET: api/Register/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Register>> GetUserById(int id)
        {
            Register user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Email, Name FROM users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new Register
                            {
                                Name = reader.GetString(2),
                                Email = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
