using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly string connectionString = "Data Source=HTRIN;Initial Catalog=DBMovie;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; // Kết nối tới cơ sở dữ liệu

        // GET: api/genres
        [HttpGet]
        public IActionResult GetGenres()
        {
            List<Genre> genres = new List<Genre>();

            string query = "SELECT id, genre_name FROM genres"; // Truy vấn lấy danh sách thể loại

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Genre genre = new Genre
                        {
                            Id = reader.GetInt32(0),
                            Genre_Name = reader.GetString(1),
                            Movies = GetMoviesByGenre(reader.GetInt32(0)) // Lấy danh sách phim theo thể loại
                        };
                        genres.Add(genre);
                    }
                }
            }

            return Ok(genres);
        }

        private List<Movie> GetMoviesByGenre(int genreId)
        {
            List<Movie> movies = new List<Movie>();
            string query = "SELECT id, title, description, release_year, video_url, genres, filters, countries, HinhAnh FROM movies WHERE genres LIKE @genreId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    // Lấy tên thể loại từ bảng genres dựa trên genreId
                    string genreName = GetGenreNameById(genreId);
                    command.Parameters.AddWithValue("@genreId", "%" + genreName + "%");

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Movie movie = new Movie
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            ReleaseYear = reader.GetInt32(3),
                            VideoUrl = reader.GetString(4),
                            Genres = reader.GetString(5),
                            Filters = reader.GetString(6),
                            Countries = reader.GetString(7),
                            HinhAnh = reader.GetString(8)
                        };
                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }

        private string GetGenreNameById(int genreId)
        {
            string genreName = string.Empty;
            string query = "SELECT genre_name FROM genres WHERE id = @genreId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genreId", genreId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        genreName = reader.GetString(0);
                    }
                }
            }

            return genreName;
        }


        // POST: api/genres
        [HttpPost]
        public IActionResult CreateGenre([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            string query = "INSERT INTO genres (genre_name) VALUES (@genre_name)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genre_name", genre.Genre_Name);
                    command.ExecuteNonQuery();
                }
            }

            return CreatedAtAction(nameof(GetGenres), new { id = genre.Id }, genre);
        }

        // PUT: api/genres/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            string query = "UPDATE genres SET genre_name = @genre_name WHERE id = @id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@genre_name", genre.Genre_Name);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound(new { message = "Genre not found" });
                    }
                }
            }

            return NoContent(); // Thành công, không trả về dữ liệu
        }

        // DELETE: api/genres/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            string query = "DELETE FROM genres WHERE id = @id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound(new { message = "Genre not found" });
                    }
                }
            }

            return NoContent(); // Thành công, không trả về dữ liệu
        }
    }
}
