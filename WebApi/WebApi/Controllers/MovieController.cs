using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApi.Model;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly string connectionString = "Data Source=HTRIN;Initial Catalog=DBMovie;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    // GET: api/movies
    [HttpGet]
    public IActionResult GetMovies()
    {
        string query = "SELECT id, title, description, release_year, video_url, genres, filters, countries, HinhAnh FROM movies";
        List<Movie> movies = new List<Movie>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
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
                        HinhAnh = reader.IsDBNull(8) ? null : reader.GetString(8) // Handle URL (string)
                    };
                    movies.Add(movie);
                }
            }
        }

        return Ok(movies);
    }

    // POST: api/movies
    [HttpPost]
    public IActionResult CreateMovie([FromBody] Movie movie)
    {
        string query = "INSERT INTO movies (title, description, release_year, video_url, genres, filters, countries, HinhAnh) " +
                       "VALUES (@title, @description, @release_year, @video_url, @genres, @filters, @countries, @HinhAnh)";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@release_year", movie.ReleaseYear);
                command.Parameters.AddWithValue("@video_url", movie.VideoUrl);
                command.Parameters.AddWithValue("@genres", movie.Genres);
                command.Parameters.AddWithValue("@filters", movie.Filters);
                command.Parameters.AddWithValue("@countries", movie.Countries);
                command.Parameters.AddWithValue("@HinhAnh", movie.HinhAnh); // Image URL

                command.ExecuteNonQuery();
            }
        }

        return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);
    }

    // PUT: api/movies/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] Movie movie)
    {
        string query = "UPDATE movies SET title = @title, description = @description, release_year = @release_year, " +
                       "video_url = @video_url, genres = @genres, filters = @filters, countries = @countries, HinhAnh = @HinhAnh " +
                       "WHERE id = @id";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@release_year", movie.ReleaseYear);
                command.Parameters.AddWithValue("@video_url", movie.VideoUrl);
                command.Parameters.AddWithValue("@genres", movie.Genres);
                command.Parameters.AddWithValue("@filters", movie.Filters);
                command.Parameters.AddWithValue("@countries", movie.Countries);
                command.Parameters.AddWithValue("@HinhAnh", movie.HinhAnh); // Image URL

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    return NotFound(new { message = "Movie not found" });
                }
            }
        }

        return NoContent(); // Return a 204 No Content status code
    }

    // DELETE: api/movies/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        string query = "DELETE FROM movies WHERE id = @id";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    return NotFound(new { message = "Movie not found" });
                }
            }
        }

        return NoContent(); // Return a 204 No Content status code
    }
}
