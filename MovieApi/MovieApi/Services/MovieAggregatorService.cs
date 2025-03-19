using System.Net.Http;
using System.Text.Json;
using DotNetEnv;
using Microsoft.Extensions.Caching.Memory;
using MovieApi.Helpers;
using MovieApi.Models;
using MovieApi.Models.GetMovieDetail;
using MovieApi.Models.GetMovieList;

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
            var cinemaWorldTask = _movieProviderService.FetchMovieListAsync(MovieProvider.CinemaWorld);
            var filmWorldTask = _movieProviderService.FetchMovieListAsync(MovieProvider.FilmWorld);

            var results = await Task.WhenAll(cinemaWorldTask, filmWorldTask);

            var cinemaWorldMovies = results[0].Select(m =>
            {
                m.Provider = MovieProvider.CinemaWorld;
                return m;
            }).ToList();

            var filmWorldMovies = results[1].Select(m =>
            {
                m.Provider = MovieProvider.FilmWorld;
                return m;
            }).ToList();

            var groupedMoviesByTitle = cinemaWorldMovies
                .Concat(filmWorldMovies)
                .GroupBy(m => m.Title)
                .Select((group, index) =>
                {
                    var representativeMovie = group.First();

                    return new MovieSummaryWithProviders
                    {
                        ID = index + 1,
                        Title = representativeMovie.Title,
                        Year = representativeMovie.Year,
                        Type = representativeMovie.Type,
                        Poster = representativeMovie.Poster,

                        ProviderDetails = group.Select(m => new MovieProviderDetail
                        {
                            ProviderID = m.Provider,
                            ProviderName = ProviderHelper.GetProviderName(m.Provider),
                            MovieID = m.ID
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

        public async Task<GetMovieDetailResponse> GetMovieDetailAsync(GetMovieDetailRequest request)
        {
            var tasks = request.MovieSourceInfos
                .Select(provider => _movieProviderService.FetchMovieDetailAsync(provider.ProviderID, provider.MovieID))
                .ToList();

            var results = await Task.WhenAll(tasks);

            if (results.Length == 0)
            {
                return new GetMovieDetailResponse();
            }

            var movieDetail = results[0];

            if (string.IsNullOrEmpty(movieDetail.Awards))
            {
                movieDetail.Awards = results
                    .Skip(1)
                    .Select(m => m.Awards)
                    .FirstOrDefault(a => !string.IsNullOrEmpty(a)) ?? null;
            }

            var providerPrices = results
                .Where(result => result != null)
                .Select(result => new ProviderPrice
                {
                    Provider = result.Provider,
                    ProviderName = ProviderHelper.GetProviderName(result.Provider),
                    PriceValue = decimal.Parse(result.Price)
                })
                .ToList();


            var response = new GetMovieDetailResponse
            {
                Title = movieDetail.Title,
                Year = movieDetail.Year,
                Rated = movieDetail.Rated,
                Released = movieDetail.Released,
                Runtime = movieDetail.Runtime,
                Genre = movieDetail.Genre,
                Director = movieDetail.Director,
                Writer = movieDetail.Writer,
                Actors = movieDetail.Actors,
                Plot = movieDetail.Plot,
                Language = movieDetail.Language,
                Country = movieDetail.Country,
                Awards = movieDetail.Awards,
                Poster = movieDetail.Poster,
                Metascore = movieDetail.Metascore,
                Rating = movieDetail.Rating,
                Votes = movieDetail.Votes,
                Type = movieDetail.Type,
                ProviderPrices = providerPrices
            };

            return response;
        }
    }
}