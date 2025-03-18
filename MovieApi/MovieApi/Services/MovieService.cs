using DotNetEnv;
using MovieApi.Helpers;
using MovieApi.Models;
using System.Net.Http;
using System.Text.Json;

namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;
        private readonly string _movieApiUrl;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiToken = Environment.GetEnvironmentVariable("API_ACCESS_TOKEN")
                ?? throw new InvalidOperationException($"Missing required API token: {_apiToken} in .env file.");
            _movieApiUrl = Environment.GetEnvironmentVariable("MOVIE_API_URL")
                ?? throw new InvalidOperationException($"Missing required Movie API Url: {_movieApiUrl} in .env file.");
        }

        public async Task<IEnumerable<MovieSummary>> GetMovieListAsync()
        {
            var cinemaWorldTask = GetMoviesAsync(MovieProvider.CinemaWorld);
            var filmWorldTask = GetMoviesAsync(MovieProvider.FilmWorld);

            var results = await Task.WhenAll(cinemaWorldTask, filmWorldTask);

            var cinemaWorldMovies = results[0].Select(m => {
                m.Providers ??= new List<MovieProvider>();
                m.Providers.Add(MovieProvider.CinemaWorld);
                return m;
            });

            var filmWorldMovies = results[1].Select(m => {
                m.Providers ??= new List<MovieProvider>();
                m.Providers.Add(MovieProvider.FilmWorld);
                return m;
            });

            var mergedMovieList = cinemaWorldMovies.Concat(filmWorldMovies)
                .GroupBy(m => m.Title)
                .Select(group =>
                {
                    var movie = group.First();
                    movie.Providers = group.SelectMany(m => m.Providers).Distinct().ToList();
                    return movie;
                })
                .ToList();

            return mergedMovieList;
        }

        private async Task<IEnumerable<MovieSummary>> GetFilmworldMoviesAsync()
        {
            return (List<MovieSummary>) await GetMoviesAsync(MovieProvider.FilmWorld);
        }

        private async Task<IEnumerable<MovieSummary>> GetCinemaworldMoviesAsync()
        {
            return (List<MovieSummary>) await GetMoviesAsync(MovieProvider.CinemaWorld);
        }

        private async Task<IEnumerable<MovieSummary>> GetMoviesAsync(MovieProvider provider)
        {
            string url = $"{_movieApiUrl}/{ProviderHelper.GetProviderName(provider)}/movies"; 
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(_apiToken))
            {
                request.Headers.Add("x-access-token", _apiToken);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovieResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Movies ?? new List<MovieSummary>();
        }
    }
}
