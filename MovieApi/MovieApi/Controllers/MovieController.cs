using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Models.GetMovieDetail;
using MovieApi.Models.GetMovieList;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieAggregatorService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieAggregatorService movieService)
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
        [HttpGet("movies2")]
        public async Task<ActionResult<GetMovieListResponse2>> GetMovieListAsync2()
        {
            var movies = await _movieService.GetMovieListAsync2();

            if (movies == null)
            {
                return NotFound("No movies found.");
            }

            return Ok(movies);
        }


        [HttpPost("movie")]
        public async Task<ActionResult<GetMovieDetailResponse>> GetMovieDetailAsync([FromBody] GetMovieDetailRequest request)
        {
            var movieDetail = await _movieService.GetMovieDetailAsync(request);

            if (movieDetail == null)
            {
                return NotFound("No movie detail found.");
            }

            return Ok(movieDetail);
        }
    }
}