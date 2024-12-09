using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApi.Model;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string _connectionString;

        public LoginController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBMovie");
        }

        // POST: api/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login loginUser)
        {
            if (loginUser == null || string.IsNullOrEmpty(loginUser.Name) || string.IsNullOrEmpty(loginUser.Password))
            {
                return BadRequest("Name and password must be provided.");
            }

            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn để lấy thông tin người dùng theo tên (Name)
                using (SqlCommand command = new SqlCommand("SELECT Id, Name, Password, User_Type FROM users WHERE Name = @Name", connection))
                {
                    command.Parameters.AddWithValue("@Name", loginUser.Name);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Password = reader.GetString(2),  // Đảm bảo lấy thông tin mật khẩu
                                //UserType = reader.GetString(3)
                            };
                        }
                    }
                }
            }

            // Kiểm tra mật khẩu
            if (user == null || user.Password != loginUser.Password)
            {
                return Unauthorized("Invalid name or password.");
            }

            // Trả về thông tin người dùng bao gồm cả Name và Password
            return Ok(new { Name = user.Name, Password = user.Password });
        }
    }
}
