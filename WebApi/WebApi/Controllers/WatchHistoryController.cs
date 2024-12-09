using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApi.Model;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchHistoryController : ControllerBase
    {
        private readonly string _connectionString;

        public WatchHistoryController(IConfiguration configuration)
        {
            // Lấy chuỗi kết nối từ appsettings.json
            _connectionString = configuration.GetConnectionString("DBMovie"); // Cập nhật chuỗi kết nối từ DBMovie
        }

        // GET: api/WatchHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchHistory>>> GetWatchHistory()
        {
            var watchHistoryList = new List<WatchHistory>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
            SELECT wh.Id, wh.User_Id, wh.Movie_Id, 
                   u.Email, u.Name, u.User_Type, 
                   m.Title, m.Description, m.Release_Year, m.Video_Url, m.Genres, m.Filters, m.Countries, m.HinhAnh
            FROM watch_history wh
            JOIN users u ON wh.User_Id = u.Id
            JOIN movies m ON wh.Movie_Id = m.Id
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var watchHistory = new WatchHistory
                            {
                                Id = reader.GetInt32(0),
                                User_Id = reader.GetInt32(1),
                                Movie_Id = reader.GetInt32(2),

                                User = new User
                                {
                                    Id = reader.GetInt32(1),  // Đảm bảo ánh xạ đúng User_Id
                                    Email = reader.GetString(3),
                                    Name = reader.GetString(4),
                                    //UserType = reader.GetString(5)
                                },

                                Movie = new Movie
                                {
                                    Id = reader.GetInt32(2),  // Đảm bảo ánh xạ đúng Movie_Id
                                    Title = reader.GetString(6),
                                    Description = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    ReleaseYear = reader.GetInt32(8),
                                    VideoUrl = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Genres = reader.IsDBNull(10) ? null : reader.GetString(10),
                                    Filters = reader.IsDBNull(11) ? null : reader.GetString(11),
                                    Countries = reader.IsDBNull(12) ? null : reader.GetString(12),
                                    HinhAnh = reader.IsDBNull(13) ? null : reader.GetString(13)
                                }
                            };

                            watchHistoryList.Add(watchHistory);
                        }
                    }
                }
            }

            return Ok(watchHistoryList);
        }

        // POST: api/WatchHistory
        [HttpPost]
        public async Task<ActionResult<WatchHistory>> CreateWatchHistory(WatchHistory watchHistory)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để thêm lịch sử xem phim mới
                string query = "INSERT INTO watch_history (User_Id, Movie_Id) OUTPUT INSERTED.Id VALUES (@User_Id, @Movie_Id)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User_Id", watchHistory.User_Id);
                    command.Parameters.AddWithValue("@Movie_Id", watchHistory.Movie_Id);

                    watchHistory.Id = (int)await command.ExecuteScalarAsync();
                }
            }

            return CreatedAtAction("GetWatchHistory", new { id = watchHistory.Id }, watchHistory);
        }

        // DELETE: api/WatchHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatchHistory(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Truy vấn SQL để xóa lịch sử xem phim
                string query = "DELETE FROM watch_history WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }
    }
}
