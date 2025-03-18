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
        public async Task<ActionResult<IEnumerable<MovieSummary>>> GetMovieListAsync()
        {
            var movies = await _movieService.GetMovieListAsync();

            if (!movies.Any())
            {
                return NotFound("No movies found.");
            }

            return Ok(movies);
        }

        [HttpGet("movie/{id}")]
        public async Task<IActionResult> GetMovieDetailAsync(int id)
        {
            return null;
        }
    }
}
