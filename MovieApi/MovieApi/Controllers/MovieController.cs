using Microsoft.AspNetCore.Mvc;
using MovieApi.Models.GetMovieList;
using MovieApi.Services;

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
        public async Task<ActionResult<GetMovieListResponse>> GetMovieListAsync()
        {
            var movies = await _movieService.GetMovieListAsync();

            if (movies == null)
            {
                return NotFound("No movies found.");
            }

            return Ok(movies);
        }

        //[HttpGet("movie/{id}")]
        //public async Task<ActionResult> GetMovieDetailAsync(int id)
        //{
        //    var movieDetail = await _movieService.GetMovieAsync();

        //    if (movieDetail == null)
        //    {
        //        return NotFound("No movie detail found.");
        //    }

        //    return Ok(movieDetail);
        //}
    }
}
