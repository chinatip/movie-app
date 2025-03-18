using DotNetEnv;
using MovieApi.Helpers;
using MovieApi.Models;
using MovieApi.Models.GetMovieList;
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

        public async Task<GetMovieListResponse> GetMovieListAsync()
        {
            var cinemaWorldTask = FetchMoviesAsync(MovieProvider.CinemaWorld);
            var filmWorldTask = FetchMoviesAsync(MovieProvider.FilmWorld);

            var results = await Task.WhenAll(cinemaWorldTask, filmWorldTask);

            var cinemaWorldMovies = results[0].Select(m => {
                m.Provider = MovieProvider.CinemaWorld;
                return m;
            }).ToList();

            var filmWorldMovies = results[1].Select(m => {
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

                        ProviderDetails = group.Select(m => new ProviderDetail
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

        //private async Task<IEnumerable<MovieSummary>> FetchMovieAsync(MovieProvider provider, string id)
        //{
        //    string url = $"{_movieApiUrl}/{ProviderHelper.GetProviderName(provider)}/movie/{id}";
        //    var request = new HttpRequestMessage(HttpMethod.Get, url);

        //    if (!string.IsNullOrEmpty(_apiToken))
        //    {
        //        request.Headers.Add("x-access-token", _apiToken);
        //    }

        //    var response = await _httpClient.SendAsync(request);
        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<FetchMoviesResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Movies ?? new List<MovieSummary>();
        //}

        private async Task<IEnumerable<MovieSummary>> FetchMoviesAsync(MovieProvider provider)
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
            return JsonSerializer.Deserialize<FetchMoviesResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Movies ?? new List<MovieSummary>();
        }
    }
}
