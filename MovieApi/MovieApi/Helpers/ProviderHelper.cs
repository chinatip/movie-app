using MovieApi.Models;

namespace MovieApi.Helpers
{
    public static class ProviderHelper
    {
        public static string GetProviderName(MovieProvider provider)
        {
            return provider switch
            {
                MovieProvider.CinemaWorld => "CinemaWorld",
                MovieProvider.FilmWorld => "FilmWorld",
                _ => throw new ArgumentException("Unknown provider", nameof(provider))
            };
        }
    }
}
