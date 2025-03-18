using MovieApi.Models;
using MovieApi.Models.GetMovieDetail;
using MovieApi.Models.GetMovieList;

namespace MovieApi.Services
{
    public interface IMovieAggregatorService
    {
        Task<GetMovieListResponse> GetMovieListAsync();

        Task<GetMovieDetailResponse> GetMovieDetailAsync(GetMovieDetailRequest request);
    }
}
