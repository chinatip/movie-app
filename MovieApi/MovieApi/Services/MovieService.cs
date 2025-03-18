using DotNetEnv;
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

        public async Task<IEnumerable<MovieSummary>> GetCinemaworldMoviesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_movieApiUrl}/cinemaworld/movies");

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
