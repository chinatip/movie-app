using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieSummary>> GetCinemaworldMoviesAsync();
    }
}
