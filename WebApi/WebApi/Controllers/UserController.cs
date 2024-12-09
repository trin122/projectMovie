using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            // Lấy chuỗi kết nối từ appsettings.json
            _connectionString = configuration.GetConnectionString("DBMovie"); // Cập nhật chuỗi kết nối từ DBMovie
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để lấy tất cả người dùng
                using (SqlCommand command = new SqlCommand("SELECT Id, Email, Password, Name FROM users", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2),
                                Name = reader.GetString(3)
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để lấy người dùng theo ID
                using (SqlCommand command = new SqlCommand("SELECT Id, Email, Password, Name FROM users WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2),
                                Name = reader.GetString(3)
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

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Nếu không hợp lệ, trả về lỗi
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để thêm người dùng mới
                string query = "INSERT INTO users (Email, Password, Name) OUTPUT INSERTED.Id VALUES (@Email, @Password, @Name)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);  // Không mã hóa mật khẩu nữa
                    command.Parameters.AddWithValue("@Name", user.Name);

                    user.Id = (int)await command.ExecuteScalarAsync();
                }
            }

            // Trả về kết quả tạo người dùng mới, sử dụng CreatedAtAction để trả về thông tin người dùng mới
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            // Kiểm tra tính hợp lệ của dữ liệu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để cập nhật người dùng
                string query = "UPDATE users SET Email = @Email, Password = @Password, Name = @Name WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);  // Không mã hóa mật khẩu nữa
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Id", user.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return NoContent(); // Cập nhật thành công
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để xóa người dùng
                string query = "DELETE FROM users WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound(); // Không tìm thấy người dùng để xóa
                    }
                }
            }

            return NoContent(); // Xóa thành công
        }
    }
}
