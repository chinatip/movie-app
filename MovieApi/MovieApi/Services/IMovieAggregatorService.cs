using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IMovieAggregatorService
    {
        Task<GetMovieListResponse> GetMovieListAsync();
    }
}