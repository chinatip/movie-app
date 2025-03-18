using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetCinemaworldMoviesAsync()
        {
            var movies = await _movieService.GetCinemaworldMoviesAsync();

            if (movies == null || !movies.Any())
            {
                return NotFound("No movies found.");
            }

            return Ok(movies);
        }

        [HttpGet("movie/{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
