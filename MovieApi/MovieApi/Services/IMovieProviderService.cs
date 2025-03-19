using MovieApi.Models;
using MovieApi.Models.GetMovieDetail;

namespace MovieApi.Services
{
    public interface IMovieProviderService
    {
        Task<FetchMovieDetailResponse> FetchMovieDetailAsync(MovieProvider provider, string id);

        Task<IEnumerable<MovieSummary>> FetchMovieListAsync(MovieProvider provider);
    }
}