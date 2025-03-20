using MovieApi.Helpers;
using MovieApi.Models;
using Sprache;

namespace MovieApi.Services
{
    public class MovieAggregatorService : IMovieAggregatorService
    {
        private readonly IMovieProviderService _movieProviderService;

        public MovieAggregatorService(IMovieProviderService movieProviderService)
        {
            _movieProviderService = movieProviderService;
        }

        public async Task<GetMovieListResponse> GetMovieListAsync()
        {
            // Fetch movie list
            var fetchMoviesTasks = new[]
                {
                    _movieProviderService.FetchMovieListAsync(MovieProvider.CinemaWorld),
                    _movieProviderService.FetchMovieListAsync(MovieProvider.FilmWorld)
                };

            var moviesResults = await Task.WhenAll(fetchMoviesTasks);
            var cinemaWorldMovies = moviesResults[0];
            var filmWorldMovies = moviesResults[1];

            // Fetch detail of each movie
            var cinemaWorldMovieDetailTasks = cinemaWorldMovies
                .Select(movie => _movieProviderService.FetchMovieDetailAsync(MovieProvider.CinemaWorld, movie.ID))
                .ToArray();

            var filmWorldMovieDetailTasks = filmWorldMovies
                .Select(movie => _movieProviderService.FetchMovieDetailAsync(MovieProvider.FilmWorld, movie.ID))
                .ToArray();

            var movieDetailResults = await Task.WhenAll(cinemaWorldMovieDetailTasks.Concat(filmWorldMovieDetailTasks));

            var groupedMoviesByTitle = movieDetailResults
                .GroupBy(m => m.Title)
                .Select((group, index) =>
                {
                    var representativeMovie = group.First();

                    return new MovieDetail
                    {
                        ID = index + 1,
                        Title = representativeMovie.Title,
                        Year = representativeMovie.Year,
                        Rated = representativeMovie.Rated,
                        Released = representativeMovie.Released,
                        Runtime = representativeMovie.Runtime,
                        Genre = representativeMovie.Genre,
                        Director = representativeMovie.Director,
                        Writer = representativeMovie.Writer,
                        Actors = representativeMovie.Actors,
                        Plot = representativeMovie.Plot,
                        Language = representativeMovie.Language,
                        Country = representativeMovie.Country,
                        Awards = representativeMovie.Awards,
                        Poster = representativeMovie.Poster,
                        Metascore = representativeMovie.Metascore,
                        Rating = representativeMovie.Rating,
                        Votes = representativeMovie.Votes,
                        Type = representativeMovie.Type,
                        Prices = group.Select(m => new Price
                        {
                            Provider = m.Provider,
                            ProviderName = ProviderHelper.GetProviderName(m.Provider),
                            Value = decimal.Parse(m.Price)
                        }).ToList()
                    };
                })
                .ToList();

            var response = new GetMovieListResponse
            {
                MovieList = groupedMoviesByTitle
            };

            return response;
        }

    }
}