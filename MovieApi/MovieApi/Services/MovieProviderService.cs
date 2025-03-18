using MovieApi.Helpers;
using MovieApi.Models.GetMovieDetail;
using MovieApi.Models.GetMovieList;
using MovieApi.Models;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace MovieApi.Services
{
    public class MovieProviderService: IMovieProviderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;
        private readonly string _movieApiUrl;

        public MovieProviderService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _apiToken = Environment.GetEnvironmentVariable("API_ACCESS_TOKEN")
                ?? throw new InvalidOperationException($"Missing required API token: {_apiToken} in .env file.");
            _movieApiUrl = Environment.GetEnvironmentVariable("MOVIE_API_URL")
                ?? throw new InvalidOperationException($"Missing required Movie API Url: {_movieApiUrl} in .env file.");
        }

        public async Task<FetchMovieDetailResponse> FetchMovieDetailAsync(MovieProvider provider, string id)
        {
            string url = $"{_movieApiUrl}/{ProviderHelper.GetProviderName(provider)}/movie/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(_apiToken))
            {
                request.Headers.Add("x-access-token", _apiToken);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var fetchedMovieDetail = JsonSerializer.Deserialize<FetchMovieDetailResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new FetchMovieDetailResponse();
            fetchedMovieDetail.Provider = provider;


            return fetchedMovieDetail;
        }

        public async Task<IEnumerable<MovieSummary>> FetchMovieListAsync(MovieProvider provider)
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
            return JsonSerializer.Deserialize<FetchMovieListResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Movies ?? new List<MovieSummary>();
        }
    }
}
