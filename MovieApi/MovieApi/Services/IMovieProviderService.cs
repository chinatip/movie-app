using MovieApi.Models.GetMovieDetail;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IMovieProviderService
    {
        Task<FetchMovieDetailResponse> FetchMovieDetailAsync(MovieProvider provider, string id);

        Task<IEnumerable<MovieSummary>> FetchMovieListAsync(MovieProvider provider);
    }
}
