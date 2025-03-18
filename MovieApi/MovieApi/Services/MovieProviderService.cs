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
        private readonly IMemoryCache _cache;
        private readonly string _apiToken;
        private readonly string _movieApiUrl;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public MovieProviderService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
            _apiToken = Environment.GetEnvironmentVariable("API_ACCESS_TOKEN")
                ?? throw new InvalidOperationException($"Missing required API token: {_apiToken} in .env file.");
            _movieApiUrl = Environment.GetEnvironmentVariable("MOVIE_API_URL")
                ?? throw new InvalidOperationException($"Missing required Movie API Url: {_movieApiUrl} in .env file.");
        }

        public async Task<FetchMovieDetailResponse> FetchMovieDetailAsync(MovieProvider provider, string movieId)
        {
            string cacheKey = $"MovieDetail_{provider}_{movieId}";
            string endpoint = $"{ProviderHelper.GetProviderName(provider)}/movie/{movieId}";

            var fetchedMovieDetail = await FetchFromApi<FetchMovieDetailResponse>(endpoint, cacheKey)
                ?? new FetchMovieDetailResponse();

            fetchedMovieDetail.Provider = provider;
            return fetchedMovieDetail;
        }

        public async Task<IEnumerable<MovieSummary>> FetchMovieListAsync(MovieProvider provider)
        {
            string cacheKey = $"MovieList_{provider}";
            string endpoint = $"{ProviderHelper.GetProviderName(provider)}/movies";

            var fetchedMovies = await FetchFromApi<FetchMovieListResponse>(endpoint, cacheKey);
            return fetchedMovies?.Movies ?? new List<MovieSummary>();
        }

        private async Task<T?> FetchFromApi<T>(string endpoint, string cacheKey)
        {
            if (_cache.TryGetValue(cacheKey, out T cachedData))
            {
                return cachedData;
            }

            string url = $"{_movieApiUrl}/{endpoint}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(_apiToken))
            {
                request.Headers.Add("x-access-token", _apiToken);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result =  JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result != null)
            {
                _cache.Set(cacheKey, result, _cacheDuration);
            }

            return result;
        }
    }
}
