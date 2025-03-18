using MovieApi.Models.GetMovieList;

namespace MovieApi.Services
{
    public interface IMovieService
    {
        Task<GetMovieListResponse> GetMovieListAsync();

        //Task<MovieDetail> GetMovieAsync();
    }
}
